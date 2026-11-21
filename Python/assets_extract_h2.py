from deppth import deppth
import subprocess
import sys
import os

print(sys.argv)
return_code = subprocess.call(r'deppth ex "'+os.path.normpath(sys.argv[2]+"\Content\Packages\\1080p\\"+sys.argv[1]+r'.pkg')+'" -t '+sys.argv[3]+"\\"+sys.argv[1]+r'"', shell=True)