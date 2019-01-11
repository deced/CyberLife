using CyberLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    public enum Directions
    {
        TopLeft = 1,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left = 8,

        None = 0
    }
    public enum Actions
    {
        CheckEnergy = 1,
        Photosynthesis,
        Extraction,
        DoDescendant,
        Eat,
        Move,
        ForsedReproduction = 7,
        None = 0
    }

    public class GenotypeState : IState
    {

        #region fields

        #endregion


        #region properties

        #endregion


        #region methods

        /// <summary>
        /// Обновляет действия бота в соответствии с геномом
        /// </summary>
        /// <param name="metadata"></param>
        public void Update(Simple2DWorld world)
        {
            int height = world.Map.LifeForms.GetLength(0);
            int width = world.Map.LifeForms.GetLength(1);
            Parallel.For(0, height, y =>
            {
            for (int x = 0; x < width; x++)
            {
                if (world.Map.LifeForms[x, y] != null)
                    {
                        if (world.Map.LifeForms[x, y].EnergyState == EnergyStates.ForsedReproduction)
                        {
                            world.Map.LifeForms[x, y].Action = Actions.ForsedReproduction;
                            NextStep(world.Map.LifeForms[x, y]);
                        }
                        else
                        {
                            SetAction(world.Map.LifeForms[x, y]);
                        }
                    }
                }
            });
        }



        /// <summary>
        /// Устанавливает действие бота в соответствии с геномом
        /// </summary>
        /// <param name="lifeForm"></param>
        private void SetAction(BotLifeForm lifeForm)
        {
            // нет реализации не конечных действий - все действия конечные
            switch (lifeForm.Genom[lifeForm.YTK])
            {
                case 1:
                    lifeForm.Action = Actions.CheckEnergy;
                    NextStep(lifeForm);
                    lifeForm.Direction = (Directions)((lifeForm.Genom[lifeForm.YTK] / 8) + 1);
                    NextStep(lifeForm);
                    // SetAction(lifeForm);
                    break;
                case 2:
                    lifeForm.Action = Actions.Photosynthesis;
                    NextStep(lifeForm);
                    break;
                case 3:
                    lifeForm.Action = Actions.Extraction;
                    NextStep(lifeForm);
                    break;
                case 4:
                    lifeForm.Action = Actions.DoDescendant;
                    NextStep(lifeForm);
                    lifeForm.Direction = (Directions)((lifeForm.Genom[lifeForm.YTK] / 8) + 1);
                    NextStep(lifeForm);
                    break;
                case 5:
                    lifeForm.Action = Actions.Eat;
                    NextStep(lifeForm);
                    lifeForm.Direction = (Directions)((lifeForm.Genom[lifeForm.YTK] / 8) + 1);
                    NextStep(lifeForm);
                    break;
                case 6:
                    lifeForm.Action = Actions.Move;
                    NextStep(lifeForm);
                    lifeForm.Direction = (Directions)((lifeForm.Genom[lifeForm.YTK] / 8) + 1);
                    NextStep(lifeForm);
                    break;
                default:
                    try
                    {
                        lifeForm.YTK = Convert.ToByte(((lifeForm.YTK + lifeForm.Genom[lifeForm.YTK]) % 63));
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Недопустимое значение YTK", (((lifeForm.YTK + lifeForm.Genom[lifeForm.YTK]) % 63)).ToString(), e);
                    }
                    lifeForm.Action = Actions.None;
                    lifeForm.Direction = Directions.None;
                    NextStep(lifeForm);
                    // SetAction(lifeForm);
                    break;

            }
        }

        public void NextStep(BotLifeForm bot)
        {
            bot.YTK = (byte)((bot.YTK + 1) % 64);
        }

        #endregion


        #region constructors

        public GenotypeState()
        {

        }

        #endregion
    }
}