from HadesMapper import HadesMapper
import subprocess
import sys

#py compile_h1.py "G:\Hades\Content\Win\Maps\A_Boss01" "G:\Hades\Content\Win\Maps\A_Boss01"
print(sys.argv)
return_code = subprocess.call(r'HadesMapper ec -i "'+sys.argv[1]+'.thing_text" -o "'+sys.argv[2]+'.thing_bin"', shell=True)