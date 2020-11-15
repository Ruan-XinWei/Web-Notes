#include <stdio.h>
#include <sys/types.h>
#include <sys/sem.h>
#include <sys/shm.h>
#include <sys/wait.h>
#include <unistd.h>
#include <stdlib.h>

union semun
{
    int val;
    struct semid_ds *buf;
    unsigned short *array;
};

//定义信号量
int mutex;
int apple;
int orange;

int main()
{
    int father, mother, son, daughter;

    //定义信号量数据结构
    struct sembuf P, V;
    union semun arg;

    //定义共享存储ID
    int apple_num_id;
    int orange_num_id;

    //定义内存虚拟地址
    int *apple_num;
    int *orange_num;

    //创建共享内存
    apple_num_id = shmget(IPC_PRIVATE, sizeof(int), IPC_CREAT | 0666);
    orange_num_id = shmget(IPC_PRIVATE, sizeof(int), IPC_CREAT | 0666);

    //初始化共享内存
    apple_num = (int *)shmat(apple_num_id, 0, 0);
    *apple_num = 4;
    orange_num = (int *)shmat(orange_num_id, 0, 0);
    *orange_num = 3;

    //创建信号量
    mutex = semget(IPC_PRIVATE, 1, IPC_CREAT | 0666);
    apple = semget(IPC_PRIVATE, 1, IPC_CREAT | 0666);
    orange = semget(IPC_PRIVATE, 1, IPC_CREAT | 0666);

    //初始化信号量
    arg.val = 1;
    if (semctl(mutex, 0, SETVAL, arg) == -1)
    {
        perror("semctl setval error");
    }
    arg.val = 0;
    if (semctl(apple, 0, SETVAL, arg) == -1)
    {
        perror("semctl setval error");
    }
    arg.val = 0;
    if (semctl(orange, 0, SETVAL, arg) == -1)
    {
        perror("semctl setval error");
    }

    //初始化P，V操作
    P.sem_num = 0;
    P.sem_op = -1;
    P.sem_flg = SEM_UNDO;
    V.sem_num = 0;
    V.sem_op = 1;
    V.sem_flg = SEM_UNDO;

    if ((father = fork()) > 0)
    {
        int i = 0;
        while (i++ < *apple_num)
        { //放规定次数苹果
            printf("father has an apple\n");
            semop(mutex, &P, 1); //对mutex进行P操作
            printf("-----father puts this apple\n");
            semop(apple, &V, 1); //对apple进行V操作
            sleep(1);
        }
        //sleep(3);    //等待其他进程完成
        exit(0);
    }
    else
    {
        if ((mother = fork()) > 0)
        {
            int i = 0;
            while (i++ < *orange_num)
            { //放规定次数桔子
                printf("mother has an orange\n");
                semop(mutex, &P, 1); //对mutex进行P操作
                printf("-----mother puts this orange\n");
                semop(orange, &V, 1); //对orange进行V操作
                sleep(1);
            }
            //sleep(3);    //等待其他进程完成
            exit(0);
        }
        else
        {
            if ((son = fork()) > 0)
            {
                int i = 0;
                while (1)
                {
                    ++i;
                    if (i > *orange_num)
                    { //取完桔子退出
                        break;
                    }
                    printf("son gets an orange\n");
                    semop(orange, &P, 1); //对orange进行P操作
                    printf("-----son eats this orange\n");
                    semop(mutex, &V, 1); //对mutex进行V操作
                }
                exit(0);
            }
            else
            {
                if ((daughter = fork()) > 0)
                {
                    int i = 0;
                    while (1)
                    {
                        if (++i > *apple_num)
                        { //取完苹果退出
                            break;
                        }
                        printf("daughter gets an apple\n");
                        semop(apple, &P, 1); //对apple进行P操作
                        printf("-----daughter eats this apple\n");
                        semop(mutex, &V, 1); //对mutex进行V操作
                    }
                    exit(0);
                }
            }
        }
    }
    wait(0);
    wait(0);
    wait(0);
    wait(0);
    exit(0);
    return 0;
}