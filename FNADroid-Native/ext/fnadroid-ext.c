#include "fnadroid-ext.h"

#include <errno.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

#include <jni.h>

#include <android/log.h>

JavaVM* GetJavaVM() {
	return javaVM;
}

JNIEnv* GetJNIEnv() {
	if (!javaVM) {
		LOGE("javaVM is null, JNI_OnLoad possibly failed!");
		return NULL;
	}
	JNIEnv* env;
	(*javaVM)->GetEnv(javaVM, (void**) &env, JNI_VERSION_1_4);
	return env;
}

int AttachThreadToJavaVM() {
	JNIEnv* env = GetJNIEnv();
	if (!env) {
		LOGE("GetJNIEnv returned null, can't attach thread!");
		return 0;
	}
	(*javaVM)->AttachCurrentThread(javaVM, &env, NULL);
	return 1;
}

int DetachThreadToJavaVM() {
	JNIEnv* env = GetJNIEnv();
		if (!env) {
		LOGE("GetJNIEnv returned null, can't detach thread!");
		return 0;
	}
	(*javaVM)->DetachCurrentThread(javaVM);
	return 1;
}

JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM* jvm, void* reserved) {
	javaVM = jvm;
	JNIEnv* env;
	int result = (*jvm)->GetEnv(jvm, (void**) &env, JNI_VERSION_1_4);
    if (result != JNI_OK) {
		LOGE("JNI_OnLoad failed - %#x", result);
        return -1;
    }

	LOGE("JNI_OnLoad succeeded!");
    return JNI_VERSION_1_4;
}
