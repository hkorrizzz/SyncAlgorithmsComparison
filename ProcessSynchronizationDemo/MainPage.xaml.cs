using System;
using System.Diagnostics;

namespace SysProg_LAB3
{
    public partial class MainPage : ContentPage
    {
        private char symbol;
        private int number;

        private int strictTurn = 0;
        private int petersonTurn = 0;
        private bool[] interested = new bool[2];
        private bool running = false;

        private Thread strictProcess0Thread;
        private Thread strictProcess1Thread;
        private Thread petersonProcess0Thread;
        private Thread petersonProcess1Thread;

        private int strictLastProcess = -1;
        private int strictConsecutiveCount0 = 0;
        private int strictConsecutiveCount1 = 0;
        private int petersonLastProcess = -1;
        private int petersonConsecutiveCount0 = 0;
        private int petersonConsecutiveCount1 = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void StartAlgorithms()
        {
            running = true;

            strictLastProcess = -1;
            strictConsecutiveCount0 = 0;
            strictConsecutiveCount1 = 0;
            petersonLastProcess = -1;
            petersonConsecutiveCount0 = 0;
            petersonConsecutiveCount1 = 0;

            interested[0] = false;
            interested[1] = false;

            strictProcess0Thread = new Thread(() => StrictProcess0());
            strictProcess1Thread = new Thread(() => StrictProcess1());
            petersonProcess0Thread = new Thread(() => PetersonProcess0());
            petersonProcess1Thread = new Thread(() => PetersonProcess1());

            strictProcess0Thread.Start();
            strictProcess1Thread.Start();   
            petersonProcess0Thread.Start();
            petersonProcess1Thread.Start();
        }

        private void StrictProcess0()
        {
            while (running)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {  
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс ожидает входа в критическую область."; labelGet0.FontSize = 10; StrictProcess0Info.Add(labelGet0);
                });
                if (running) Thread.Sleep(1000);
                StrictTryEnter(0);
                
                if (running)
                {
                    UpdateStrictConsecutiveCount(0);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 0 вошел в критическую область."; labelGet0.FontSize = 10; StrictProcess0Info.Add(labelGet0);
                    });
                    char result = (char)((int)symbol + 2);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Измененный символ ({result}) был возвращен.";  labelGet0.FontSize = 10; StrictProcess0Info.Add(labelGet0);
                    });
                    if (running) Thread.Sleep(1000);    
                }

                StrictLeave(0);
                if (running)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                    Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 0 вышел из критической области."; labelGet0.FontSize = 10; StrictProcess0Info.Add(labelGet0);
                    });
                    if (running) Thread.Sleep(1000);
                }
            }
        }

        private void StrictProcess1()
        {
            while (running)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                        Label labelGet1 = new Label(); labelGet1.Text = $"{ReturnTime()} Процесс ожидает входа в критическую область."; labelGet1.FontSize = 10; StrictProcess1Info.Add(labelGet1);
                
                });

                StrictTryEnter(1);
            

                if (running)
                {
                    UpdateStrictConsecutiveCount(1);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 1 вошел в критическую область."; labelGet0.FontSize = 10; StrictProcess1Info.Add(labelGet0);
                    });
                    for (int i = 0; i < number; i++)
                    {
                        Console.Beep();
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Сигнал №{i} был подан."; labelGet0.FontSize = 10; StrictProcess1Info.Add(labelGet0);
                        });
                    }
                }
                if (running) Thread.Sleep(1000);
                if (running) { 
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Label labelGet1 = new Label(); labelGet1.Text = $"{ReturnTime()} Процесс 1 вышел из критической области."; labelGet1.FontSize = 10; StrictProcess1Info.Add(labelGet1);
                        });
                }
                StrictLeave(1);
                if (running) Thread.Sleep(1000);
            }
        }

        private void StrictTryEnter(int process)
        {

            while (running && strictTurn != process)
            {
                Thread.Sleep(100);
            }   
        }

        private void StrictLeave(int process)
        {
            strictTurn = 1 - process;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                StrictTurnValue.Text = $"{strictTurn}";
            });

        }



        private void PetersonProcess0()
        {
            while (running)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    UpdatePetersonConsecutiveCount(0);
                    Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс ожидает входа в критическую область."; labelGet0.FontSize = 10; PetersonProcess0Info.Add(labelGet0);
                });
                if (running) Thread.Sleep(2000); /////////

                PetersonTryEnter(0);

                if (running)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 0 вошел в критическую область."; labelGet0.FontSize = 10; PetersonProcess0Info.Add(labelGet0);
                    });
                    char result = (char)((int)symbol + 2);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Измененный символ ({result}) был возвращен."; labelGet0.FontSize = 10; PetersonProcess0Info.Add(labelGet0);
                    });
                    if (running) Thread.Sleep(1500); //////////////
                }

                PetersonLeave(0);
                if (running)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 0 вышел из критической области."; labelGet0.FontSize = 10; PetersonProcess0Info.Add(labelGet0);
                    });
                    if (running) Thread.Sleep(1000);
                }

            }
        }

        private void PetersonProcess1()
        {
            while(running)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Label labelGet1 = new Label(); labelGet1.Text = $"{ReturnTime()} Процесс ожидает входа в критическую область."; labelGet1.FontSize = 10; PetersonProcess1Info.Add(labelGet1);
                });

                PetersonTryEnter(1);

                if (running)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        UpdatePetersonConsecutiveCount(1);
                        Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Процесс 1 вошел в критическую область."; labelGet0.FontSize = 10; PetersonProcess1Info.Add(labelGet0);
                    });
                    for (int i = 0; i < number; i++)
                    {
                        Console.Beep();
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Label labelGet0 = new Label(); labelGet0.Text = $"{ReturnTime()} Сигнал №{i } был подан."; labelGet0.FontSize = 10; PetersonProcess1Info.Add(labelGet0);
                        });
                    }
                }
                if (running) Thread.Sleep(1000);

                if (running)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Label labelGet1 = new Label(); labelGet1.Text = $"{ReturnTime()} Процесс 1 вышел из критической области."; labelGet1.FontSize = 10; PetersonProcess1Info.Add(labelGet1);
                    });
                }
                PetersonLeave(1); 

                if (running) Thread.Sleep(1000);
            }
        }

        private void PetersonTryEnter(int process) {

            interested[process] = true;
            petersonTurn = process;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PetersonTurnValue.Text = $"{petersonTurn}";
                PetersonInterested0.Text = interested[0] ? "TRUE" : "FALSE";
                PetersonInterested1.Text = interested[1] ? "TRUE" : "FALSE";
            });
            while (running && petersonTurn == process && interested[1-process])
            {
                Thread.Sleep(100);
            }

            
        }

        private void PetersonLeave(int process)
        {
            interested[process] = false;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                PetersonInterested0.Text = interested[0] ? "TRUE" : "FALSE";
                PetersonInterested1.Text = interested[1] ? "TRUE" : "FALSE";
            });
        }



        private void StartButtonClicked(object sender, EventArgs e)
        {
 
            if (char.TryParse(SymbolEntry.Text, out symbol) && int.TryParse(NumberEntry.Text, out number) && number>=1 && number<=3)
            {
                if ((int)symbol + 2 <= 65535)
                {
                    StrictProcess0Info.Clear();
                    StrictProcess1Info.Clear();
                    PetersonProcess0Info.Clear();  
                    PetersonProcess1Info.Clear();

                    StartAlgorithms();
                }
                else
                {
                    DisplayAlert("Ошибка", "Невозможно увеличить код символа на 2", "ОК");
                    return;
                }
            }
            else
            {
                DisplayAlert("Ошибка ввода", "Проверьте корректность ввода данных!", "ОК");
                return;
            }
        }

        private void StopButtonClicked(object sender, EventArgs e)
        {
            running = false;

            strictProcess0Thread.Join();
            strictProcess1Thread.Join();
            petersonProcess0Thread.Join();  
            petersonProcess1Thread.Join();
        }

        private string ReturnTime()
        {
            return $"{DateTime.Now:HH}:{DateTime.Now:mm}:{DateTime.Now:ss}:{DateTime.Now:fff} ";
        }

        private void UpdateStrictConsecutiveCount(int currentProcess)
        {
            if (strictLastProcess == currentProcess)
            {
                if (currentProcess == 0)
                    strictConsecutiveCount0++;
                else
                    strictConsecutiveCount1++;
            }
            else
            {
                strictLastProcess = currentProcess;
                strictConsecutiveCount0 = (currentProcess == 0) ? 1 : 0;
                strictConsecutiveCount1 = (currentProcess == 1) ? 1 : 0;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                StrictProcess0Count.Text = strictConsecutiveCount0.ToString();
                StrictProcess1Count.Text = strictConsecutiveCount1.ToString();
            });
        }

        private void UpdatePetersonConsecutiveCount(int currentProcess)
        {
            if (petersonLastProcess == currentProcess)
            {
                if (currentProcess == 0)
                    petersonConsecutiveCount0++;
                else
                    petersonConsecutiveCount1++;
            }
            else
            {
                petersonLastProcess = currentProcess;
                petersonConsecutiveCount0 = (currentProcess == 0) ? 1 : 0;
                petersonConsecutiveCount1 = (currentProcess == 1) ? 1 : 0;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                PetersonProcess0Count.Text = petersonConsecutiveCount0.ToString();
                PetersonProcess1Count.Text = petersonConsecutiveCount1.ToString();
            });
        }
    }
}