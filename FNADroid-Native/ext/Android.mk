LOCAL_PATH := $(call my-dir)

###########################
#
# FNADroid extensions
#
###########################

include $(CLEAR_VARS)
LOCAL_MODULE := fnadroid-ext
LOCAL_SRC_FILES := fnadroid-ext.c
LOCAL_LDLIBS := -llog -landroid -lm -ldl
LOCAL_CFLAGS += -D_REENTRANT
include $(BUILD_SHARED_LIBRARY)
