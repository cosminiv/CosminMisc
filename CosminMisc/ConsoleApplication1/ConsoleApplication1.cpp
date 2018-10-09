#include "pch.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void ex26();

int main(int argc, char** argv) {
	clock_t start, end;
	double cpu_time_used;
	start = clock();
	/**/
	ex26();
	/**/
	end = clock();
	cpu_time_used = ((double)(end - start)) / CLOCKS_PER_SEC;
	printf("\nExecution Time - %f \n", cpu_time_used);
	return 0;
}

void ex26() {
	int n, i, len, maxlen, maxn;
	maxlen = 0;

	for (n = 2; n <= 1000; n++) {
		int rest = 1;
		int r0;
		for (i = 0; i < n; i++) 
			rest = (rest * 10) % n;
		r0 = rest;
		len = 0;

		do {
			rest = (rest * 10) % n;
			len++;
		} while (rest != r0);

		if (len > maxlen) {
			maxn = n;
			maxlen = len;
		}
	}
	printf("ex26: %d: %d\n", maxn, maxlen);
}