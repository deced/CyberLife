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
        delegate void Changed(string str, Bitmap map);

        #region field

        private byte colorType = 0; // текущий режим отображения
        private Simple2DWorld.Simple2DWorld world; // текущий экземпляр мира
        Changed changed;
        Thread thread;

        #endregion


        #region properties


        #endregion


        #region methods

        /// <summary>
        /// Обработка нажатий ColorTypeButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorTypeButton_Click(object sender, EventArgs e)
        {
            colorType++;
            if (colorType >= 3)
                colorType = 0;
            ((ColorState)world.States["ColorState"]).ColorType = (ColorType)colorType;
        }



        /// <summary>
        /// Загрузка формы MainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            thread = new Thread(UpdateMap);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            mapPicture2.MouseClick += OnPictureBoxClicked;
            sunEnergy.Value = world.NaturalPhenomena["SunPhenomen"].BaseIntensity / 10;
            mineralsEnergy.Value = world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity / 10;
            mineralsLabel.Text = world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity.ToString();
            sunLabel.Text = world.NaturalPhenomena["SunPhenomen"].BaseIntensity.ToString();
            mutationLabel.Text = BotLifeForm.MutationPercent.ToString() + " %";
            mutationPercent.Value = BotLifeForm.MutationPercent;
        }



        /// <summary>
        /// Обработка кликов по PictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPictureBoxClicked(object sender, MouseEventArgs e)
        {
            byte sunCount = 0;
            byte mineralCounts = 0;
            byte eatCount = 0;
            byte moveCount = 0;
            byte noneCount = 0;
            byte descendantCount = 0;
            byte shareCount = 0;
            var location = e.Location;
            int x = (int)Math.Round(location.X / (mapPicture2.Width / (double)world.Map.Width));
            int y = (int)Math.Round(location.Y / (mapPicture2.Height / (double)world.Map.Height));
            if (world.Map.LifeForms[x, y] != null)
            {
                BotLifeForm bot = world.Map.LifeForms[x, y];
                infoLabel.Text = "Клетка (" + x + ";" + y + ") содержит живого бота";
                infoLabel.Text += "\r\nЭнергия: " + bot.Energy + "\r\n";
                infoLabel.Text += "Номер колонии бота: " + bot.FriendId + "\r\n";
                for (int i = 0; i < 64; i++)
                {
                    infoLabel.Text += bot.Genom[i] + ", ";
                    switch (bot.Genom[i])
                    {
                        case 1:
                            sunCount++;
                            break;
                        case 2:
                            mineralCounts++;
                            break;
                        case 3:
                            descendantCount++;
                            break;
                        case 4:
                            eatCount++;
                            break;
                        case 5:
                            moveCount++;
                            break;
                        case 6:
                            shareCount++;
                            break;
                        default:
                            noneCount++;
                            break;

                    }
                    if (i % 10 == 0 && i != 0)
                        infoLabel.Text += "\r\n";

                }
                infoLabel.Text += "\r\nФотосинтеза: " + sunCount +
                    "\r\nЭкстракции: " + mineralCounts +
                    "\r\nПоедания: " + eatCount +
                    "\r\nПередвижения: " + moveCount +
                    "\r\nОтпочковывания: " + descendantCount +
                    "\r\nПередачи энергии: " + shareCount +
                    "\r\nНе назначено: " + noneCount;
            }
            else if (world.Map.Organic[x, y] != null)
            {
                infoLabel.Text = "Клетка (" + x + ";" + y + ") содержит органику";
                infoLabel.Text += "\r\nЭнергия: " + world.Map.Organic[x, y].Energy;
            }
            else
                infoLabel.Text = "Клетка (" + x + ";" + y + ") не содержит ботов";
        }



        /// <summary>
        /// Обработка скролла TrackBar энергии минералов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mineralsEnergy_Scroll(object sender, EventArgs e)
        {
            world.NaturalPhenomena["MineralsPhenomen"].BaseIntensity = mineralsEnergy.Value * 10;
            mineralsLabel.Text = (mineralsEnergy.Value * 10).ToString();
        }



        /// <summary>
        /// Обработка скролла TrackBar энергии солнца
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sunEnergy_Scroll(object sender, EventArgs e)
        {
            world.NaturalPhenomena["SunPhenomen"].BaseIntensity = sunEnergy.Value * 10;
            sunLabel.Text = (sunEnergy.Value * 10).ToString();
        }



        /// <summary>
        /// Обработка скролла TrackBar процента мутаций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mutationPercent_Scroll(object sender, EventArgs e)
        {
            BotLifeForm.MutationPercent = (byte)mutationPercent.Value;
            mutationLabel.Text = BotLifeForm.MutationPercent.ToString() + " %";
        }



        /// <summary>
        /// Вызывает отрисовку данных
        /// </summary>
        /// <param name="str"></param>
        /// <param name="map"></param>
        public void Invoke(string str, Bitmap map)
        {
            changed = new Changed(Change);
            statsLabel.Invoke(changed, new object[] { str, map });
        }



        /// <summary>
        /// Отрисовывает новую карту и изменяет содержимое statsLabel
        /// </summary>
        /// <param name="str"></param>
        /// <param name="map"></param>
        public void Change(string str, Bitmap map)
        {
            statsLabel.Text = str;
            mapPicture2.Image = map;
        }



        /// <summary>
        /// Запускает обновление всего мира
        /// </summary>
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

        #endregion


        #region constructors

        /// <summary>
        /// Инициализирует экземпляр MainForm
        /// </summary>
        /// <param name="simple2DWorld"></param>
        public MainForm(Simple2DWorld.Simple2DWorld simple2DWorld)
        {
            world = simple2DWorld;
            InitializeComponent();
            mapPicture2.Height = world.Map.Height;
            mapPicture2.Width = world.Map.Width;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            while (true)
            {
                mapPicture2.Width += world.Map.Width / 10;
                mapPicture2.Height += world.Map.Height / 10;
                if (mapPicture2.Width + 500 > this.Width || mapPicture2.Height + 70 > this.Height)
                {
                    mapPicture2.Width -= world.Map.Width / 10;
                    mapPicture2.Height -= world.Map.Height / 10;
                    break;
                }
            }
        }

        #endregion
    }
}
