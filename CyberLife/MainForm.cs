using CyberLife.Platform.World_content;
using CyberLife.Simple2DWorld;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberLife
{
    public partial class MainForm : Form
    {
        public bool IsLoading;
        Simple2DWorld.Simple2DWorld world;
        bool colortype = true;
        delegate void Changed(string str, Bitmap map);
        Changed changed;
        Thread thread;
        public MainForm(Simple2DWorld.Simple2DWorld simple2DWorld)
        {
            world = (Simple2DWorld.Simple2DWorld)simple2DWorld;
            InitializeComponent();
            mapPicture2.Height = world.Map.Height;
            mapPicture2.Width = world.Map.Width;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            while (true)
            {
                mapPicture2.Width+= world.Map.Height;
                mapPicture2.Height += world.Map.Width;
                if (mapPicture2.Width + 500 > this.Width || mapPicture2.Height + 100 > this.Height)
                {
                    mapPicture2.Width -= world.Map.Width;
                    mapPicture2.Height -= world.Map.Height;
                    break;
                }
            }

        }

        private void ColorTypeButton_Click(object sender, EventArgs e)
        {
            if (colortype)
            {
                colortype = false;
                ((ColorState)world.States["ColorState"]).ColorType = ColorType.EnergyDisplay;
            }
            else
            {
                colortype = true;
                ((ColorState)world.States["ColorState"]).ColorType = ColorType.Default;
            }
        }
        public void Invoke(string str, Bitmap map)
        {
            changed = new Changed(Change);

            statsLabel.Invoke(changed, new object[] { str, map });
        }
        public void Change(string str, Bitmap map)
        {
            statsLabel.Text = str;
            mapPicture2.Image = map;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            thread = new Thread(UpdateMap);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            mapPicture2.MouseClick += OnPictureBoxClicked;
            sunEnergy.Value = world.NaturalPhenomena["SunPhenomen"].BaseIntensity/10;
            mineralsEnergy.Value = world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity/10;
            mineralsLabel.Text = world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity.ToString();
            sunLabel.Text = world.NaturalPhenomena["SunPhenomen"].BaseIntensity.ToString(); 
            mutationLabel.Text = BotLifeForm.MutationPercent.ToString() + " %";
            mutationPercent.Value = BotLifeForm.MutationPercent;
        }

        private void OnPictureBoxClicked(object sender, MouseEventArgs e)
        {
            byte ph = 0;
            byte ex = 0;
            byte eat = 0;
            byte check = 0;
            byte move = 0;
            byte none = 0;
            byte descendant = 0;
            var location = e.Location;
            if (world.Map.LifeForms[location.X / (mapPicture2.Width / world.Map.Width), location.Y / (mapPicture2.Height / world.Map.Height)] != null)
            {
                BotLifeForm bot = world.Map.LifeForms[location.X / (mapPicture2.Width / world.Map.Width), location.Y / (mapPicture2.Height / world.Map.Height)];
                infoLabel.Text = "Клетка (" + location.X / (mapPicture2.Width / world.Map.Width) + ";" + location.Y / (mapPicture2.Height / world.Map.Height) + ") содержит живого бота";
                infoLabel.Text += "\r\nЭнергия: " + bot.Energy+"\r\n";
                for(int i = 0;i<64;i++)
                {              
                    infoLabel.Text += bot.Genom[i] + ", ";
                    switch(bot.Genom[i])
                    {
                        case 1:
                            check++;
                            break;
                        case 2:
                            ph++;
                            break;
                        case 3:
                            ex++;
                            break;
                        case 4:
                            descendant++;
                            break;
                        case 5:
                            eat++;
                            break;
                        case 6:
                            move ++;
                            break;
                        default:
                            none++;
                            break;

                    }
                    if (i % 10 ==0 && i !=0)
                        infoLabel.Text += "\r\n";

                }
                infoLabel.Text += "\r\nФотосинтеза: " + ph+"\r\nЭкстракции: "+ex + "\r\nПоедания: " + eat + "\r\nПередвижения: "+move+"\r\nПроверок энергии: " + check+"\r\nОтпочковывания: "+ descendant+"\r\nНе назначено: "+none;
            }
            else if(world.Map.Organic[location.X / (mapPicture2.Width / world.Map.Width), location.Y / (mapPicture2.Height / world.Map.Height)] != null)
            {
                infoLabel.Text = "Клетка (" + location.X / (mapPicture2.Width / world.Map.Width) + ";" + location.Y / (mapPicture2.Height / world.Map.Height) + ") содержит органику";
                infoLabel.Text += "\r\nЭнергия: " + world.Map.Organic[location.X / (mapPicture2.Width / world.Map.Width), location.Y / (mapPicture2.Height / world.Map.Height)].Energy;
            }
            else
                infoLabel.Text = "Клетка (" + location.X / (mapPicture2.Width / world.Map.Width) + ";" + location.Y / (mapPicture2.Height / world.Map.Height) + ") не содержит ботов";

        }

        public void UpdateMap()
        {
            while (true)
            {
                world.Update();
                Invoke("Кол-во форм жизни " + world.Map.LifeFormsCount.ToString() +
                    "\r\nКол-во органики " + world.Map.OrganicCount.ToString() +
                    "\r\nТекущий ход " + world.Age.ToString() +
                    "\r\nТекущий сезон " + ((SeasonsPhenomen)world.NaturalPhenomena["SeasonsPhenomen"]).CurSeason.ToString(), ((Simple2dVisualizer)world.Visualizer).Map);

            }
        }

        private void mapPicture2_MouseEnter(object sender, EventArgs e)
        {
          //  thread.Suspend();
        }

        private void mapPicture2_MouseLeave(object sender, EventArgs e)
        {
          //  thread.Resume();
        }

        private void mineralsEnergy_Scroll(object sender, EventArgs e)
        {
            world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity = mineralsEnergy.Value * 10;
            mineralsLabel.Text = (mineralsEnergy.Value * 10).ToString();
        }

        private void sunEnergy_Scroll(object sender, EventArgs e)
        {
            
            world.NaturalPhenomena["SunPhenomen"].BaseIntensity = sunEnergy.Value * 10;
            sunLabel.Text = (sunEnergy.Value * 10).ToString();
        }

        private void mutationPercent_Scroll(object sender, EventArgs e)
        {
            BotLifeForm.MutationPercent =(byte) mutationPercent.Value;
            mutationLabel.Text = BotLifeForm.MutationPercent.ToString() +" %";
        }
    }
}
