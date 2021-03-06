using ConversationalWeed.Game.Pictures.Abstractions;
using ConversationalWeed.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ConversationalWeed.Game.Pictures
{
    public class ImageGenerator : IImageGenerator
    {
        public static int CARD_WIDTH = 100;
        public static int CARD_HEIGHT = 150;
        public static int MARGIN = 20;
        public static int FIELD_TITLE_MARGIN = 5;
        public static int TEXT_HEIGHT = 80;
        public static Font DEFAULT_FONT = new Font("Tahoma", 32, FontStyle.Bold);
        public static string DEFAULT_SKIN = "default";

        public MemoryStream GenerateGameBoard(IList<Player> players)
        {
            MemoryStream ms = new MemoryStream();
            int width = 0;
            int height = 0;
            IList<Bitmap> playerBoards = new List<Bitmap>();

            foreach (Player player in players)
            {
                Bitmap playerBoard = GeneratePlayerBoard(player, players);
                width = playerBoard.Width > width ? playerBoard.Width : width;
                height += playerBoard.Height;
                playerBoards.Add(playerBoard);
            }
            Bitmap finalImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.Clear(Color.LightGreen);

                int offset = 0;
                foreach (Bitmap playerBoard in playerBoards)
                {
                    var playerBoardRect = new RectangleF(0, offset, playerBoard.Width, playerBoard.Height);
                    graphics.DrawImage(playerBoard, playerBoardRect);
                    offset += playerBoard.Height;
                }
            }

            finalImage.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        public MemoryStream GeneratePlayerHand(Player player)
        {
            MemoryStream ms = new MemoryStream();

            int numberOfCards = player.Hand.Count;
            int width = (CARD_WIDTH + MARGIN) * numberOfCards + MARGIN;
            int height = MARGIN + CARD_HEIGHT + MARGIN;
            Bitmap finalImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.Clear(Color.LightGreen);

                int cardX = MARGIN;
                int cardY = MARGIN;
                foreach (Card card in player.Hand)
                {
                    Image cardImage = GetImageCard(card.Type, player.CurrentCardSkin);
                    var cardRect = new RectangleF(cardX, cardY, cardImage.Width, cardImage.Height);
                    graphics.DrawImage(cardImage, cardRect);
                    cardX += cardImage.Width + MARGIN;
                }
            }
            finalImage.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        public MemoryStream GenerateCardPlayed(CardType cardType, string currentPlayerSkin = "default")
        {
            MemoryStream ms = new MemoryStream();

            int width = MARGIN + CARD_WIDTH + MARGIN;
            int height = MARGIN + CARD_HEIGHT + MARGIN;
            Bitmap finalImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.Clear(Color.LightGreen);

                int cardX = MARGIN;
                int cardY = MARGIN;
                Image cardImage = GetImageCard(cardType, currentPlayerSkin);
                var cardRect = new RectangleF(cardX, cardY, cardImage.Width, cardImage.Height);
                graphics.DrawImage(cardImage, cardRect);
            }
            finalImage.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        private Bitmap GeneratePlayerBoard(Player player, IList<Player> players)
        {
            int numberOfDogsOrBusted = player.Fields
                .Where(f => f.ProtectedValue == ProtectedFieldValue.Busted || f.ProtectedValue == ProtectedFieldValue.Dog)
                .ToList().Count;
            bool hasTwoRows = numberOfDogsOrBusted > 0;
            int numberOfFields = player.Fields.Count;
            int totalWidth = (numberOfFields * (CARD_WIDTH + MARGIN)) + MARGIN;
            int totalHeight = (MARGIN + TEXT_HEIGHT + FIELD_TITLE_MARGIN + CARD_HEIGHT + MARGIN + TEXT_HEIGHT + MARGIN);
            if (hasTwoRows)
            {
                totalHeight += (MARGIN + CARD_HEIGHT);
            }

            Bitmap finalImage = new Bitmap(totalWidth, totalHeight, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(finalImage))
            {
                graphics.Clear(Color.LightGreen);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                int nameWidth = totalWidth - (2 * MARGIN);
                int nameHeight = TEXT_HEIGHT;
                int nameX = MARGIN;
                int nameY = MARGIN;
                RectangleF nameRectangle = new RectangleF(nameX, nameY, nameWidth, nameHeight);
                graphics.SetText(player.Name, nameRectangle);

                int i = 0;
                foreach (Field field in player.Fields)
                {
                    int cardX = MARGIN + (CARD_WIDTH + MARGIN) * i;
                    int cardY = MARGIN + TEXT_HEIGHT + MARGIN;
                    if (hasTwoRows)
                    {
                        int topCardY = cardY;
                        cardY += CARD_HEIGHT + MARGIN;
                        if (field.ProtectedValue != ProtectedFieldValue.Free)
                        {
                            string protectedFieldSkin = GetProtectedFieldSkin(field, players);
                            Image topCardImage = GetImageCard(field.ProtectedValue, protectedFieldSkin);
                            RectangleF topCardRect = new RectangleF(cardX, topCardY, CARD_WIDTH, CARD_HEIGHT);
                            graphics.DrawImage(topCardImage, topCardRect);
                        }
                    }
                    string fieldSkin = GetFieldSkin(field, players);
                    Image cardImage = GetImageCard(field.Value, fieldSkin);
                    RectangleF cardRect = new RectangleF(cardX, cardY, CARD_WIDTH, CARD_HEIGHT);
                    graphics.DrawImage(cardImage, cardRect);

                    string fieldTitle = field.Id.ToString();
                    int fieldTitleY = cardY + CARD_HEIGHT + FIELD_TITLE_MARGIN;
                    RectangleF fieldTitleRect = new RectangleF(cardX, fieldTitleY, CARD_WIDTH, TEXT_HEIGHT);
                    graphics.SetText(fieldTitle, fieldTitleRect);
                    i++;
                }
            }
            return finalImage;
        }

        private string GetProtectedFieldSkin(Field field, IList<Player> players)
        {
            string fieldSkin = DEFAULT_SKIN;
            if (field.ProtectedValueOwnerId.HasValue)
            {
                var owner = players.FirstOrDefault(p => p.Id == field.ProtectedValueOwnerId.Value);
                if (owner != null)
                {
                    fieldSkin = owner.CurrentCardSkin;
                }
            }

            return fieldSkin;
        }

        private string GetFieldSkin(Field field, IList<Player> players)
        {
            string fieldSkin = DEFAULT_SKIN;
            if (field.ValueOwnerId.HasValue)
            {
                var owner = players.FirstOrDefault(p => p.Id == field.ValueOwnerId.Value);
                if (owner != null)
                {
                    fieldSkin = owner.CurrentCardSkin;
                }
            }

            return fieldSkin;
        }

        private Image GetImageCard(FieldValue fieldState, string cardSkin)
        {
            Image img = fieldState switch
            {
                FieldValue.OnePlant => Image.FromFile("Images/" + cardSkin + "/oneplant.jpg"),
                FieldValue.TwoPlants => Image.FromFile("Images/" + cardSkin + "/twoplant.jpg"),
                FieldValue.ThreePlants => Image.FromFile("Images/" + cardSkin + "/threeplant.jpg"),
                FieldValue.FourPlants => Image.FromFile("Images/" + cardSkin + "/fourplant.jpg"),
                FieldValue.SixPlants => Image.FromFile("Images/" + cardSkin + "/sixplant.jpg"),
                FieldValue.Dandelion => Image.FromFile("Images/" + cardSkin + "/dandelion.jpg"),
                _ => Image.FromFile("Images/empty.png"),
            };
            return img;
        }

        private Image GetImageCard(ProtectedFieldValue protectedState, string cardSkin)
        {
            Image img = protectedState switch
            {
                ProtectedFieldValue.Busted => Image.FromFile("Images/" + cardSkin + "/busted.jpg"),
                ProtectedFieldValue.Dog => Image.FromFile("Images/" + cardSkin + "/dog.jpg"),
                _ => Image.FromFile("Images/empty.png"),
            };
            return img;
        }

        private Image GetImageCard(CardType cardType, string cardSkin = "default")
        {
            Image img = cardType switch
            {
                CardType.Weed1 => Image.FromFile("Images/" + cardSkin + "/oneplant.jpg"),
                CardType.Weed2 => Image.FromFile("Images/" + cardSkin + "/twoplant.jpg"),
                CardType.Weed3 => Image.FromFile("Images/" + cardSkin + "/threeplant.jpg"),
                CardType.Weed4 => Image.FromFile("Images/" + cardSkin + "/fourplant.jpg"),
                CardType.Weed6 => Image.FromFile("Images/" + cardSkin + "/sixplant.jpg"),
                CardType.Dandileon => Image.FromFile("Images/" + cardSkin + "/dandelion.jpg"),
                CardType.WeedKiller => Image.FromFile("Images/" + cardSkin + "/weedkiller.jpg"),
                CardType.Stealer => Image.FromFile("Images/" + cardSkin + "/stealer.jpg"),
                CardType.Monzon => Image.FromFile("Images/" + cardSkin + "/monzon.jpg"),
                CardType.Hippie => Image.FromFile("Images/" + cardSkin + "/hippie.jpg"),
                CardType.Dog => Image.FromFile("Images/" + cardSkin + "/dog.jpg"),
                CardType.Busted => Image.FromFile("Images/" + cardSkin + "/busted.jpg"),
                CardType.Potzilla => Image.FromFile("Images/" + cardSkin + "/potzilla.jpg"),
                _ => Image.FromFile("Images/empty.png"),
            };
            return img;
        }
    }
}
