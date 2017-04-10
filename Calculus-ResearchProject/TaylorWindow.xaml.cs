using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculus_ResearchProject {
    /// <summary>
    /// Interaction logic for TaylorWindow.xaml
    /// </summary>
    public partial class TaylorWindow : Window {
        public TaylorWindow() {
            InitializeComponent();
        }

        private void B_Calculate_Click( object sender, RoutedEventArgs e ) {

            Taylor TaylorMethod;

            try {
                switch ( CB_Method.SelectedIndex ) {

                    case 0:
                        TaylorMethod = new E( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 1:
                        TaylorMethod = new Sine( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 2:
                        TaylorMethod = new Cosine( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 3:
                        TaylorMethod = new Tangent( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 4:
                        TaylorMethod = new Cosecant( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 5:
                        TaylorMethod = new Secant( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    case 6:
                        TaylorMethod = new Cotangent( Int16.Parse( TB_NumofSums.Text ), Decimal.Parse( TB_XInput.Text ) );
                        TB_Output.Text = TaylorMethod.ToString();
                        break;
                    default:
                        TB_Output.Text = "Please choose and option";
                        break;
                }
            } catch ( DivideByZeroException ) {
                TB_Output.Text = "Can not divide by zero.";
            } catch ( OverflowException ) {
                TB_Output.Text = "Numbers too large to handle";
            } catch ( Exception ) {
                TB_Output.Text = "Invalid Input";
            }

        }
    }

    public abstract class Taylor {

        protected int K;
        protected Decimal X;

        public Taylor( int Num, Decimal x ) {
            K = Num;
            X = x;
        }

        protected static Decimal Factorial( int x ) {
            return x > 1 ? x * Factorial( --x ) : 1;
        }

        public static Decimal Power( Decimal Base, int P ) {
            if ( P == 0 )
                return 1;
            if ( Base == 0 )
                return 0;
            return P % 2 == 0 ? Power( Base * Base, P >> 1 ) : Base * Power( Base * Base, P >> 1 );
        }

        protected abstract Decimal Value();

        public override string ToString() {
            return this.Value().ToString();
        }

    }

    public class E : Taylor {

        public E( int Num, Decimal x ) : base( Num, x ) { }

        private static Func<Decimal, int, Decimal> F = ( Decimal x, int n ) => { return ( 1 / Factorial( n ) ) * Power( x, n ); };

        protected override Decimal Value() {
            Decimal Sum = 0;
            for ( int i = 0; i < K; ++i ) {
                Sum += F( X, i );
            }
            return Sum;
        }

    }

    public class Sine : Taylor {

        public Sine( int Num, Decimal x ) : base( Num, x ) { }

        protected internal static Func<Decimal, int, Decimal> F = ( Decimal x, int n ) => { return ( Power( -1, n ) / ( Factorial( ( 2 * n ) + 1 ) ) ) * Power( x, ( 2 * n ) + 1 ); };

        protected override Decimal Value() {
            Decimal Sum = 0;
            for ( int i = 0; i < K; ++i ) {
                Sum += F( X, i );
            }
            return Sum;
        }

        public static implicit operator Decimal( Sine S ) {
            return S.Value();
        }

    }

    public class Cosine : Taylor {

        public Cosine( int Num, Decimal x ) : base( Num, x ) { }

        protected internal static Func<Decimal, int, Decimal> F = ( Decimal x, int n ) => { return ( Power( -1, n ) / ( Factorial( 2 * n ) ) ) * Power( x, 2 * n ); };

        protected override Decimal Value() {
            Decimal Sum = 0;
            for ( int i = 0; i < K; ++i ) {
                Sum += F( X, i );
            }
            return Sum;
        }

        public static implicit operator Decimal(Cosine C) {
            return C.Value();
        }

    }

    public class Tangent : Taylor {

        public Tangent( int Num, Decimal x ) : base( Num, x ) { }

        protected override Decimal Value() {
            return new Sine( K, X ) / new Cosine( K, X );
        }
    }

    public class Cosecant : Sine {

        public Cosecant( int Num, Decimal x ) : base( Num, x ) { }

        protected override Decimal Value() {
            return 1 / base.Value();
        }
    }

    public class Secant : Cosine {

        public Secant( int Num, Decimal x ) : base( Num, x ) { }
        
        protected override Decimal Value() {
            return 1 / base.Value();
        }
    }

    public class Cotangent : Tangent {

        public Cotangent( int Num, Decimal x ) : base( Num, x ) { }

        protected override Decimal Value() {
            return 1 / base.Value();
        }
    }

}