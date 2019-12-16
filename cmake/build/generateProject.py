#!/usr/bin/env python3

import os
import sys
import datetime
import timeit

def retrieveSrcDir():
    src_dir  = os.path.realpath(__file__)
    for i in range (3):
        (src_dir,tail) = os.path.split(src_dir)
    return src_dir

def retrieveWorkingDir():
    return os.path.join(os.path.split(retrieveSrcDir())[0],sys.argv[1])
   
if __name__ == '__main__':
   
    print('\n')
    print('\tSystem version: ' + sys.version)
    
    t = timeit.default_timer()

    print("\tTarget folder: " + sys.argv[1])

    # Retrieve root directory & build directory
    src_dir = retrieveSrcDir()       
    build_dir = os.path.join(os.path.split(src_dir)[0],sys.argv[1])
    
    print('\tSource directory:' + src_dir)
    print('\tBuild directory: ' + build_dir)

    # Create build directory if non existing
    if not os.path.exists(build_dir):
        os.mkdir (build_dir)   
    os.chdir(build_dir)
   
    # Generate cmake batch command
    print('\n')
    print('\tGenerating cmake command')
    cmd = "cmake %s -G \"%s\"" %(src_dir,sys.argv[2])
    for argument in sys.argv[3:] :
        if argument:
            print('\t\tCommand found:' + argument)
            cmd = "%s \"%s\"" %(cmd, argument)
            
    print("\tStarting %s" % cmd)
    print('\n')
    
    # Execute cmake batch command
    os.system( cmd )

    print("Script run {time}".format(time=datetime.timedelta(seconds=timeit.default_timer() - t)))