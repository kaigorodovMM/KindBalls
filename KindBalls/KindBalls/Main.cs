/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace KindBalls
{
    public class Program : Window
    {
        public class Bird
        {

            const double g = 9.8;
            const double pi = 3.1415;
            double v, alpha, T;
            double t, x = 0, y = 0;

            public void read() // ввод параметров полёта
            {
                Console.Write("Введите скорость v = ");
                v = Convert.ToDouble(Console.ReadLine());
                Console.Write("Введите угол альфа = ");
                alpha = Convert.ToDouble(Console.ReadLine());
                alpha = alpha * pi / 180;
                T = 2 * v * Math.Sin(alpha) / g; // время падения
            }

            public void position_write() // вывод координат
            {
                Console.WriteLine("x = " + Convert.ToString(x) + ";        " + "y = " + Convert.ToString(y));
            }
            double k(double t)
            {
                return 0.01;
            }
            public void fly(double n1, double n2)
            {
                v = n1;
                alpha = n2;
                alpha = alpha * pi / 180;
                T = 2 * v * Math.Sin(alpha) / g;
                

                Rectangle pep = new Rectangle(50, 90, 0, 2);
                double vx = v * Math.Cos(alpha);
                double vy = v * Math.Sin(alpha);

                for (t = 0; y >= 0; t += 0.04)
                {
                    x = x + 0.04 * vx;
                    y = y + 0.04 * vy;
                    vx = vx - 0.04 * k(t) * vx; // m = 1 kg;
                    vy = vy - 0.04 * (g + k(t) * vy);
                    if (x >= pep.x1 && x <= pep.x2 && y >= pep.y1 && y <= pep.y2)
                    {
                        position_write();
                        //Console.WriteLine("Столкновение");
                        Cout.Invoke();
                        break;
                    }
                    if (y >= 0) position_write();
                }
                Console.WriteLine("Полёт завершён");

            }
            public delegate void Collisions();
            public event Collisions Cout;

        }

        public class Rectangle
        {
            public double x1, x2, y1, y2;
            public Rectangle(double x1, double x2, double y1, double y2)
            {
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
            }
        }

        public class Return1
        {
            public void Collision_cout()
            {
                //Console.WriteLine("Столкновение");
                MessageBox.Show("Столкновение");
            }
        }
        /*public static void Main(string[] args)
        {
            Bird voron1 = new Bird();
            Return1 cout1 = new Return1();


            voron1.Cout += cout1.Collision_cout;
            voron1.fly();
        }*/

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Program());
        }
        public Program()
        {
            Title = "Главное окно";
            Button start_btn = new Button();
            start_btn.Content = "Старт полёта";
            
            StackPanel stack = new StackPanel();
            stack.Background = Brushes.Aquamarine;
            Content = stack;

            TextBox txtbox_alpha = new TextBox();
            TextBlock txtblock_alpha = new TextBlock();
            txtblock_alpha.Text = "Введите угол:";
            
            StackPanel stack_alpha = new StackPanel();
            stack_alpha.Orientation = Orientation.Horizontal;
            stack_alpha.Children.Add(txtblock_alpha);
            stack_alpha.Children.Add(txtbox_alpha);

            TextBox txtbox_v = new TextBox();
            TextBlock txtblock_v = new TextBlock();
            txtblock_v.Text = "Введите скорость:";
            StackPanel stack_v = new StackPanel();
            stack_v.Orientation = Orientation.Horizontal;
            stack_v.Children.Add(txtblock_v);
            stack_v.Children.Add(txtbox_v);


            stack.Children.Add(stack_alpha);
            stack.Children.Add(stack_v);
            stack.Children.Add(start_btn);
            start_btn.Click += ButtonOnClick;

            /*  изменение размеров Content по размерам окна 
            Viewbox view = new Viewbox();
            Content = view;
            view.Child = stack;
            */

            DockPanel dock = new DockPanel();
            Content = dock;
            // Создание меню           
            Menu menu = new Menu();
            MenuItem item1 = new MenuItem();
            item1.Header = "Игра";
            menu.Items.Add(item1);

            MenuItem item2 = new MenuItem();
            item2.Header = "Справка";
            menu.Items.Add(item2);

            // Размещение меню у верхнего края панели            
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            

            // Создание строки состояния             
            StatusBar status = new StatusBar();
            StatusBarItem statitem = new StatusBarItem();
            statitem.Content = "Число очков: 0";
            status.Items.Add(statitem);
            // Размещение строки состояния у нижнего края панели             
            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            DockPanel.SetDock(stack, Dock.Left);
            dock.Children.Add(stack);

            void ButtonOnClick(object sender, RoutedEventArgs args)
            {
                Bird voron1 = new Bird();
                Return1 cout1 = new Return1();
               

                voron1.Cout += cout1.Collision_cout;
                voron1.fly(Convert.ToDouble(txtbox_alpha.Text), Convert.ToDouble(txtbox_v.Text));
            }

        }

        

        

    }
}
