/* timer.c -- Пример создания таймера. */
#include <signal.h>
#include <stdio.h> 
#include <string.h>
#include <windows.h> 
#include <time.h>
#include <sys/utime.h>
#include <sys/types.h>
#include <sys/timeb.h>
#include <timeapi.h>

void timer_handler(int signum)
{
	static int count = 0;
	printf("timer expired %d times\n", ++count);
}
int main()
{
	struct sigaction sa;
	struct itimerval timer;
	/* Назначение функции timer_handler обработчиком сигнала
	SIGVTALRM. */
	memset(&sa, 0, sizeof(sa));
	sa.sa_handler = &timer_handler;
	sigaction(SIGVTALRM, &sa, NULL);
	/* Таймер сработает через 250 миллисекунд... */
	timer.it_value.tv_sec = 0;
	timer.it_value.tv_usec = 250000;
	/* ... и будет продолжать активизироваться каждые 250
	миллисекунд. */
	timer.it_interval.tv_sec = 0;
	timer.it_interval.tv_usec = 250000;
	/* Запуск виртуального таймера. Он подсчитывает фактической время работы процесса. */
	setitimer(ITIMER_VIRTUAL, &timer, NULL);
	while (1);
}
}
