namespace ConsoleApplication3delegate
{
    class DepositCard
    {
        public int amount;

        public void Display()
        {
            Console.WriteLine("储蓄卡余额为：{0}", amount);
        }
        public void Account(int balance, int payday)
        {
            amount += balance;
            Console.WriteLine("今天是本月的｛0｝，取款｛1｝，储蓄卡余额为：{2}。", DateTime.Today.Day, balance, amount);
        }
    }

        class CreditCard
        {
            private int billamount;
            private int repaymentday;
            private int amount;//信用卡余额
            private DepositCard pos;//绑定储蓄卡

            public void repay()
            {
                if ((pos.amount - billamount) < Math.Abs(billamount))
                {
                    Console.WriteLine("绑定的储蓄卡余额不足喽，无法还款喔！");
                }
                else
                {
                    billamount = 0;
                    Console.WriteLine("还款成功啦！");
                }
            }


            public CreditCard(int billamount, int repaymentday,int amount, DepositCard pos)
            {
                this.billamount = billamount;
                this.amount = amount + billamount;
                this.pos = pos;
                this.repaymentday = repaymentday;
            }

            public int getbillamount() { return billamount; }       //账单对应的金额


            public int getrepaymentday() { return repaymentday; }   //还款的日期

            public void Display() { Console.WriteLine("信用卡余额为：{0}", billamount); }


        }

    class CreditCardDelegate
    {
        public int billamount;
        public int repaymentday;
        public delegate void Depositrepayment(int a, int b);    //储蓄卡还款定义
        public delegate void Creditrepayment();     //信用卡账单清除定义
        public event Depositrepayment Moneysth;     //储蓄卡还款绑定
        public event Creditrepayment Amountsth;     //信用卡账单清除绑定

        public void sth1(int p, int q)
        {
            // 如果事件不为 null
            if (Moneysth != null)
            {
                Console.WriteLine("正在触发储蓄卡还款：");
                Console.WriteLine("\n###################################");
                // 触发事件
                Moneysth(p, q);

            }
        }
        public void sth2()
        {
            // 如果事件不为 null
            if (Amountsth != null)
            {
                Console.WriteLine("正在触发信用卡还款：");
                Console.WriteLine("\n###################################");
                // 触发事件
                Amountsth();

            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            DepositCard depositCard = new DepositCard();
            depositCard.amount = 10000;
            CreditCard creditCard1 = new CreditCard(-2000, 9, 10000, depositCard);
            CreditCard creditCard2 = new CreditCard(-3000, 13, 10000, depositCard);
            CreditCard creditCard3 = new CreditCard(-5000, 29, 10000, depositCard);
            depositCard.Display(); Console.WriteLine("");
            List<CreditCard> Cards = new List<CreditCard>();
            Cards.Add(creditCard1); Cards.Add(creditCard2); Cards.Add(creditCard3);
            CreditCardDelegate ytt = new CreditCardDelegate();//创建委托对象

            foreach (CreditCard card in Cards)
            {
                Console.WriteLine("信用卡开始执行委托还款。。。。。。");
                CreditCardDelegate.Depositrepayment a1 = new CreditCardDelegate.Depositrepayment(depositCard.Account);//储蓄卡扣款
                CreditCardDelegate.Creditrepayment a2 = new CreditCardDelegate.Creditrepayment(card.repay);//信用卡账单清除

                ytt.Moneysth += a1;//绑定对应事件
                ytt.Amountsth += a2;
                //开始执行相应的事件
                ytt.sth1(card.getbillamount(), card.getrepaymentday());
                ytt.sth2();

                ytt.Moneysth -= a1;//解绑对应的事件
                ytt.Amountsth -= a2;
              


                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}
