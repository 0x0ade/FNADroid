LOCAL_PATH := $(call my-dir)/../../FNADroid-FNA/lib/FAudio

include $(CLEAR_VARS)
LOCAL_MODULE    := FAudio
LOCAL_SRC_FILES := \
	src/F3DAudio.c \
	src/FAudio.c \
	src/FAudio_internal.c \
	src/FAudio_internal_simd.c \
	src/FAudioFX_reverb.c \
	src/FAudioFX_volumemeter.c \
	src/FACT.c \
	src/FACT3D.c \
	src/FACT_internal.c \
	src/FAPOBase.c \
	src/FAPOFX.c \
	src/FAPOFX_eq.c \
	src/FAPOFX_masteringlimiter.c \
	src/FAPOFX_reverb.c \
	src/FAPOFX_echo.c \
	src/FAudio_platform_sdl2.c
LOCAL_SHARED_LIBRARIES := SDL2
include $(BUILD_SHARED_LIBRARY)
