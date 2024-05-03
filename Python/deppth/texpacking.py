from pathlib import Path
from .entries import AtlasEntry
from .utils import requires
import json
import os

try: import PIL.Image
except ImportError: pass

try: import scipy.spatial
except ImportError: pass

try: import PyTexturePacker
except ImportError: pass

@requires('PIL.Image', 'PyTexturePacker')
def build_atlases(source_dir, target_dir, basename, size, include_hulls=False):
  files = find_files(source_dir)
  hulls = {}
  namemap = {}
  for filename in files:
    # Build hulls for each image so we can store them later
    if include_hulls:
      hulls[filename.name] = get_hull_points(filename)
    else:
      hulls[filename.name] = []
    namemap[filename.name] = str(filename)

  # Perfom the packing. This will create the spritesheets and primitive atlases, which we'll need to turn to usable ones
  packer = PyTexturePacker.Packer.create(max_width=size[0], max_height=size[1], bg_color=0x00000000, atlas_format='json', 
    enable_rotated=False, trim_mode=1, border_padding=0, shape_padding=0)
  packer.pack(files, f'{basename}%d', target_dir)
  
  return (hulls, namemap)

def find_files(source_dir):
  file_list = []
  for path in [path for path in Path(source_dir).rglob('*.png') if not 'atlases' in str(path)]:
    file_list.append(path)
  return file_list

@requires('scipy.spatial')
def get_hull_points(path):
  im = PIL.Image.open(path)
  points = []

  width, height = im.size
  for x in range(width):
    for y in range(height):
      a = im.getpixel((x, y))[3]
      if a > 4:
        points.append((x, y))

  if (len(points)) > 0:
    try:
      hull = scipy.spatial.ConvexHull(points)
    except:
      return [] # Even if there are points this can fail if e.g. all the points are in a line
    vertices = []
    for vertex in hull.vertices:
      x, y = points[vertex]
      vertices.append((x,y))
    
    return vertices
  else:
    return []

def transform_atlas(filename, namemap, hulls={}, source_dir='', target_dir=''):
  with open(filename) as f:
    ptp_atlas = json.load(f)
    frames = ptp_atlas['frames']
    atlas = AtlasEntry()
    atlas.version = 4
    atlas.name = f'bin\\Win\\Atlases\\{os.path.splitext(filename)[0]}'
    atlas.referencedTextureName = atlas.name
    atlas.isReference = True
    atlas.subAtlases = []

    for texture_name in frames:
      frame = frames[texture_name]
      subatlas = {}
      subatlas['name'] = os.path.splitext(os.path.relpath(namemap[texture_name], source_dir))[0]
      subatlas['topLeft'] = {'x': frame['spriteSourceSize']['x'], 'y': frame['spriteSourceSize']['y']}
      subatlas['originalSize'] = {'x': frame['sourceSize']['w'], 'y': frame['sourceSize']['h']}
      subatlas['rect'] = {
        'x': frame['frame']['x'],
        'y': frame['frame']['y'],
        'width': frame['frame']['w'],
        'height': frame['frame']['h']
      }
      subatlas['scaleRatio'] = {'x': 1.0, 'y': 1.0}
      subatlas['isMulti'] = False
      subatlas['isMip'] = False
      subatlas['isAlpha8'] = False
      subatlas['hull'] = transform_hull(hulls[texture_name], subatlas['topLeft'], (subatlas['rect']['width'], subatlas['rect']['height']))
      atlas.subAtlases.append(subatlas)

  atlas.export_file(os.path.join(target_dir, f'{os.path.splitext(filename)[0]}.atlas.json'))
    
def transform_hull(hull, topLeft, size):
  # There are two transforms to do. First, we need to subtract the topLeft offset values
  # to account for the shifting of the hull as the result of that.
  # Then, we need to subtract half the width and height from x and y of each point because
  # the hull values appear to be designed to be such that 0,0 is the center of the image, not
  # the top-left like most coordinate systems

  def transform_point(point):
    x = point[0] - topLeft['x'] - round(size[0]/2.0)
    y = point[1] - topLeft['y'] - round(size[1]/2.0)
    return [x, y]

  new_hull = []
  for point in hull:
    new_hull.append(transform_point(point))

  return new_hull