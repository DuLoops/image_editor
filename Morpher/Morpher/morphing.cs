using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Morpher
{

    class morphing
    {

        Vector PQ;
        double d;
        int p = 0;
        int b = 2;
        double a = 0.01;

        Vector CreateVector(Vector p, Vector q) {
            return new Vector(q.X - p.X, q.Y - p.Y);
        }

        Vector CreateNormal(Vector v)
        {
            return new Vector(v.Y * -1, v.X);
        }

        double Magnitude(Vector v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        double DotProduct(Vector a, Vector b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }

        double ProjectionMagnitude(Vector top, Vector bot)
        {
            return DotProduct(top, bot) / Magnitude(bot);
        }

        public Vector getSourceP(Vector P, Vector Q, Vector T, Vector P_, Vector Q_)
        {
            PQ = CreateVector(P, Q);
            Vector n = CreateNormal(PQ);

            Vector P_Q_ = CreateVector(P_, Q_);
            Vector n_ = CreateNormal(P_Q_);

            Vector TP = CreateVector(T,P);
            Vector PT = CreateVector(P, T);

            d = ProjectionMagnitude(PT, n);
            double fl = ProjectionMagnitude(PT, PQ) / Magnitude(PQ);
            Vector T_;
            T_.X = P_.X + (fl * P_Q_.X) + (d * (n_.X / Magnitude(n_)));
            T_.Y = P_.Y + (fl * P_Q_.Y) + (d * (n_.Y / Magnitude(n_)));
            return T_;
        }

        public double getWeight()
        { 
            return (1/ Math.Pow((Math.Abs(d) + a), b));
        }

    }
}
