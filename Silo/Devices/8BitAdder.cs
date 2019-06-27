// ReSharper disable MemberCanBePrivate.Global
namespace Silo.Devices
{
    /// <summary>
    /// Input Map:<para/>
    /// 7: A1<para/>
    /// 6: B1<para/>
    /// 5: C1<para/>
    /// 4: D1<para/>
    /// 3: E1<para/>
    /// 2: F1<para/>
    /// 1: G1<para/>
    /// 0: H1<para/>
    /// <para/>
    /// 15: A2<para/>
    /// 14: B2<para/>
    /// 13: C2<para/>
    /// 12: D2<para/>
    /// 11: E2<para/>
    /// 10: F2<para/>
    /// 9: G2<para/>
    /// 8: H2<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 8: A<para/>
    /// 7: B<para/>
    /// 6: C<para/>
    /// 5: D<para/>
    /// 4: E<para/>
    /// 3: F<para/>
    /// 2: G<para/>
    /// 1: H<para/>
    /// 0: Carry<para/>
    /// </summary>
    public class EightBitAdder : Component
    {
        private readonly Component _fa1;
        private readonly Component _fa2;
        private readonly Component _fa3;
        private readonly Component _fa4;
        private readonly Component _fa5;
        private readonly Component _fa6;
        private readonly Component _fa7;
        private readonly Component _ha;

        public const int A1In = 7;
        public const int B1In = 6;
        public const int C1In = 5;
        public const int D1In = 4;
        public const int E1In = 3;
        public const int F1In = 2;
        public const int G1In = 1;
        public const int H1In = 0;
        
        public const int A2In = 15;
        public const int B2In = 14;
        public const int C2In = 13;
        public const int D2In = 12;
        public const int E2In = 11;
        public const int F2In = 10;
        public const int G2In = 9;
        public const int H2In = 8;
        
        public const int CarryOut = 0;
        public const int AOut = 8;
        public const int BOut = 7;
        public const int COut = 6;
        public const int DOut = 5;
        public const int EOut = 4;
        public const int FOut = 3;
        public const int GOut = 2;
        public const int HOut = 1;
        
        public EightBitAdder() : base(16, 9)
        {
            _ha = new HalfAdder();
            _fa1 = new FullAdder();
            _fa2 = new FullAdder();
            _fa3 = new FullAdder();
            _fa4 = new FullAdder();
            _fa5 = new FullAdder();
            _fa6 = new FullAdder();
            _fa7 = new FullAdder();

            _ha.AttachTo(_fa1, 1, 2);
            _fa1.AttachTo(_fa2, 1, 2);
            _fa2.AttachTo(_fa3, 1, 2);
            _fa3.AttachTo(_fa4, 1, 2);
            _fa4.AttachTo(_fa5, 1, 2);
            _fa5.AttachTo(_fa6, 1, 2);
            _fa6.AttachTo(_fa7, 1, 2);
        }

        public void AttachToAll(Component comp, int offset = 0)
        {
            AttachTo(comp, 1, 0 + offset);
            AttachTo(comp, 2, 1 + offset);
            AttachTo(comp, 3, 2 + offset);
            AttachTo(comp, 4, 3 + offset);
            AttachTo(comp, 5, 4 + offset);
            AttachTo(comp, 6, 5 + offset);
            AttachTo(comp, 7, 6 + offset);
            AttachTo(comp, 8, 7 + offset);
        }
        
        public override void Update()
        {
            _ha.SetPortState(0, GetInPortState(A1In));
            _ha.SetPortState(1, GetInPortState(A2In));
                
            _fa1.SetPortState(0, GetInPortState(B1In));
            _fa1.SetPortState(1, GetInPortState(B2In));

            _fa2.SetPortState(0, GetInPortState(C1In));
            _fa2.SetPortState(1, GetInPortState(C2In));

            _fa3.SetPortState(0, GetInPortState(D1In));
            _fa3.SetPortState(1, GetInPortState(D2In));

            _fa4.SetPortState(0, GetInPortState(E1In));
            _fa4.SetPortState(1, GetInPortState(E2In));

            _fa5.SetPortState(0, GetInPortState(F1In));
            _fa5.SetPortState(1, GetInPortState(F2In));

            _fa6.SetPortState(0, GetInPortState(G1In));
            _fa6.SetPortState(1, GetInPortState(G2In));

            _fa7.SetPortState(0, GetInPortState(H1In));
            _fa7.SetPortState(1, GetInPortState(H2In));

            UpdateOutput(CarryOut, _fa7.GetPortState(1));
            UpdateOutput(HOut, _fa7.OutState());
            UpdateOutput(GOut, _fa6.OutState());
            UpdateOutput(FOut, _fa5.OutState());
            UpdateOutput(EOut, _fa4.OutState());
            UpdateOutput(DOut, _fa3.OutState());
            UpdateOutput(COut, _fa2.OutState());
            UpdateOutput(BOut, _fa1.OutState());
            UpdateOutput(AOut, _ha.OutState());
        }
    }
}