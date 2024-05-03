from deppth import deppth
import subprocess
import sys

print(sys.argv)
return_code = subprocess.call(r'deppth ex "'+sys.argv[2]+"\Content\Win\Packages\\"+sys.argv[1]+r'.pkg" -t "'+sys.argv[3]+"\\"+sys.argv[1]+r'"', shell=True)