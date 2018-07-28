LOCAL_PATH := $(call my-dir)/../../FNADroid-FNA/lib/FAudio

include $(CLEAR_VARS)
LOCAL_MODULE    := FAudio
LOCAL_SRC_FILES := src/F3DAudio.c \
	src/FAudio.c \
	src/FAudio_internal.c \
	src/FAudioFX.c \
	src/FAudioFX_internal.c \
	src/FACT.c \
	src/FACT3D.c \
	src/FACT_internal.c \
	src/FAPOBase.c \
	src/XNA_Song.c \
	src/FAudio_platform_sdl2.c
LOCAL_SHARED_LIBRARIES := SDL2
include $(BUILD_SHARED_LIBRARY)
