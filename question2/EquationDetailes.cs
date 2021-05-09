using System;
using System.Collections.Generic;
using System.Text;

namespace question2
{
    class EquationDetailes
    {
        private readonly int MAX_VALUE=20;
        private readonly int MIN_VALUE=1;

        public int[] Answers { get; set; }


        public string Equation { get; set; }

        public int Result { get; set; }

        





        public EquationDetailes()
        {
            Random random = new Random();

            int rnd1 = random.Next(MIN_VALUE, MAX_VALUE);
            int rnd2 = random.Next(MIN_VALUE, MAX_VALUE);
            Result = rnd1+rnd2;

            Answers = new int[4];
            //choose random button to be the one with the correct answers 
            int chosenBut = random.Next(0, 4);
            //put answers in Num
            for (int i = 0; i < 4; i++)
            {
                if (i == chosenBut)
                    Answers[i] = rnd1 + rnd2;
                else
                    Answers[i] = random.Next(MIN_VALUE, MAX_VALUE) * random.Next(MIN_VALUE, MAX_VALUE);


            }


            Equation = $"{rnd1} + {rnd2} = ?";

        }

    }
}
