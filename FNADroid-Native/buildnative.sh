#!/bin/bash
NATIVEDIR=$(dirname "$0")

# SDL2_image needs a modified Android.mk
echo 'Generating new SDL2_image Android.mk'
sed -e 's/LOCAL_PATH := $(call my-dir)/LOCAL_PATH := $(call my-dir)\/SDL2_image/' \
	-e 's/LOCAL_C_INCLUDES := $(LOCAL_PATH)/LOCAL_C_INCLUDES := $(LOCAL_PATH)\/..\/..\/..\/SDL2Droid-CS\/deps\/SDL\/include\nLOCAL_C_INCLUDES += $(LOCAL_PATH)\nLOCAL_ALLOW_UNDEFINED_SYMBOLS := true/' \
	"$NATIVEDIR/SDL2_image/SDL2_image/Android.mk" > "$NATIVEDIR/SDL2_image/Android.mk"

# Automatically generate prebuilts Android.mk
echo 'Generating new prebuilts Android.mk'
rm "$NATIVEDIR/prebuilts/Android.mk" 2> /dev/null
echo 'LOCAL_PATH := $(call my-dir)
' > "$NATIVEDIR/prebuilts/Android.mk"
for arch in $(basename $(echo -n "$NATIVEDIR/prebuilts/*/")); do
	# We just need one architecture - list all dirs and pick one
	for lib in $(ls "$NATIVEDIR/prebuilts/$arch"); do
		echo 'prebuilts: lib: '"$lib"
		echo \
'include $(CLEAR_VARS)
LOCAL_MODULE := '${lib:3:${#lib}-6}'
LOCAL_SRC_FILES := $(TARGET_ARCH_ABI)/'"$lib"'
include $(PREBUILT_SHARED_LIBRARY)
' >> "$NATIVEDIR/prebuilts/Android.mk"
	done
done

ndk-build -j 4 NDK_PROJECT_PATH="$NATIVEDIR/../FNADroid" NDK_APPLICATION_MK="$NATIVEDIR/Application.mk"
