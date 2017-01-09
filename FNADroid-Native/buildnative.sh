#!/bin/bash
NATIVEDIR=$(dirname "$0")
ndk-build -j 4 NDK_PROJECT_PATH="$NATIVEDIR/../FNADroid" NDK_APPLICATION_MK="$NATIVEDIR/Application.mk"
ndk-build -j 4 NDK_PROJECT_PATH="$NATIVEDIR/../FNADroid" NDK_APPLICATION_MK="$NATIVEDIR/../SDL2Droid-CS/SDL2Droid-CS-Native/Application.mk"
