using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.Game;
using Tetris.Init;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative)),
        };

        // danh sách hình 
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 300;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 20;

        public int row;
        public int col;

        private GameState gameState;
        public MainWindow(int _row, int _col, int _w, int? min = 75)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            row = _row;
            col = _col;
            minDelay = (int)min;
            gameState = new GameState(_row, _col);
            GameCanvas.Width = _w;
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        // thêm vị trí các ô vào giao diện 
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];

            // kích thước ô trên grid
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {

                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,

                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        // thêm hình ảnh các ô vào giao diện
        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        // vẽ ô vuông 
        private void DrawBlock(Block.Block block)
        {
            foreach (Position p in block.TilePosition())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        // vẽ ô sẽ xuất hiện kế tiếp
        private void DrawNextBlock(Block.BlockQueue blockQueue)
        {
            Block.Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];

        }

        // vẽ ô đang đc giữ lại 
        private void DrawHeldBlock(Block.Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }

        // vẽ ảnh sẽ rơi xuống ở vị trí nào
        private void DrawGhost(Block.Block block)
        {
            int dropDistance = gameState.BlockDropDisance();
            foreach (Position p in block.TilePosition())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhost(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            ScoreText.Text = $"Score : {gameState.Score}";
        }

        private async Task GameLoop()
        {
            Draw(gameState);
            while (!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score : {gameState.Score}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft(); break;
                case Key.Right:
                    gameState.MoveBlockRight(); break;
                case Key.Down:
                    gameState.MoveBlockDown(); break;
                case Key.Up:
                    gameState.RotateBlockCW(); break;
                case Key.Z:
                    gameState.RotateBlockCCW(); break;
                case Key.C:
                    gameState.HoldBlock(); break;
                case Key.Space:
                    gameState.DropBlock(); break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState(row, col);
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();

        }

        private void backToMain(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new Main();
            newWindow.Show();
            Close();
        }

        private void changeOpaMouseMove(object sender, MouseEventArgs e)
        {
            if (back.Opacity == 0.6)
            {
                back.Opacity = 1;
            }
            else if (back.Opacity == 1)
            {
                back.Opacity = 0.6;
            }
        }
    }
}