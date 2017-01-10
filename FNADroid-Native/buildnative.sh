#!/bin/bash
NATIVEDIR=$(dirname "$0")

# SDL2_image needs a modified Android.mk
sed -e 's/LOCAL_PATH := $(call my-dir)/LOCAL_PATH := $(call my-dir)\/SDL2_image/' \
	-e 's/LOCAL_C_INCLUDES := $(LOCAL_PATH)/LOCAL_C_INCLUDES := $(LOCAL_PATH)\/..\/..\/..\/SDL2Droid-CS\/deps\/SDL\/include\nLOCAL_C_INCLUDES += $(LOCAL_PATH)\nLOCAL_ALLOW_UNDEFINED_SYMBOLS := true/' \
	"$NATIVEDIR/SDL2_image/SDL2_image/Android.mk" > "$NATIVEDIR/SDL2_image/Android.mk"

ndk-build -j 4 NDK_PROJECT_PATH="$NATIVEDIR/../FNADroid" NDK_APPLICATION_MK="$NATIVEDIR/Application.mk"
