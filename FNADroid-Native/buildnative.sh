#!/bin/bash
ndk-build -j 4 NDK_PROJECT_PATH="$(pwd)/../SDL2Droid-CS" NDK_APPLICATION_MK="$(pwd)/Application.mk"
