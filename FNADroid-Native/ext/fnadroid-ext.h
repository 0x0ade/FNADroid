#ifndef FNADROID_EXT_H
#define FNADROID_EXT_H

#include <jni.h>

#include <android/log.h>

#define  LOG_TAG    "FNADROID-EXT"
#define  LOGI(...)  __android_log_print(ANDROID_LOG_INFO,LOG_TAG,__VA_ARGS__)
#define  LOGW(...)  __android_log_print(ANDROID_LOG_WARN,LOG_TAG,__VA_ARGS__)
#define  LOGE(...)  __android_log_print(ANDROID_LOG_ERROR,LOG_TAG,__VA_ARGS__)

JavaVM* javaVM;

JavaVM* GetJavaVM();
JNIEnv* GetJNIEnv();
int AttachThreadToJavaVM();
int DetachThreadToJavaVM();

#endif
