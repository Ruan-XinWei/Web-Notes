int WDTCTL,WDTPW,WDTHOLD;
int tab[8];
void maint(void)
{
    WDTCTL = WDTPW + WDTHOLD;
    init_port();
    init_TM1638();
    int h=23,m=59,s=45,i;
    while (1)
    {
        Write_DATA(4 * 2, tab[h/10]); //显示 0
        Write_DATA(5 * 2, tab[h%10]); //显示 1
        //Write_DATA(6 * 2, tab[2]); //显示 2
        Write_DATA(7 * 2, tab[m/10]); //显示 3
        Write_DATA(0 * 2, tab[m%10]); //显示 4
        //Write_DATA(1 * 2, tab[5]); //显示 5
        Write_DATA(2 * 2, tab[s/10]); //显示 6
        Write_DATA(3 * 2, tab[s%10]); //显示 7
        i = 16384;
        do
        {
            --i;
        } while (i);
        
        s += 1;
        m += s / 60;
        h += m / 60;
        h %= 24;
        m %= 60;
        s %= 60;

        h = (h + m/60) % 24;
    }
}