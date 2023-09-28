using IJunior.TypedScenes;

namespace Assets.Infrastructure.SceneLoadHandler
{
    public class LevelLoadingChooser
    {
        public static void LoadScene(int levelNumber, PlayerLevelData playerLevel)
        {
            switch (levelNumber)
            {
                case 0:
                    TrainingLevel.Load(playerLevel);
                    break;
                case 1:
                    Level_1.Load(null);
                    break;
                case 2:
                    Level_2.Load(playerLevel);
                    break;
                case 3:
                    Level_3.Load(playerLevel);
                    break;
                case 4:
                    Level_4.Load(playerLevel);
                    break;
                case 5:
                    Final_Level_1.Load();
                    break;
                default:
                    Menu.Load();
                    break;
            }
        }
    }
}