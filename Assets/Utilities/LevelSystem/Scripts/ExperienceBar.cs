using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Unitylities
{
    public class ExperienceBar : MonoBehaviour
    {
        private float posX;
        private float posY;
        private float posZ;

        private Slider slider;
        private Image fill;
        private TextMeshProUGUI barText;
        private Transform bar;
        private Transform background;
        private Transform text;
        private LevelSystem levelSystem;
        public Gradient gradient;

        private void Awake()
        {
            Setup();
        }
        private void Start()
        {
            if (levelSystem != null)
            {
                SetExperience(levelSystem.ExperienceNormalized);

                levelSystem.OnExperienceChanged += SubOnExperienceChanged;
                levelSystem.OnLevelChanged += SubOnLevelChanged;
                levelSystem.OnMaxLevelChanged += SubOnMaxLevelChanged;
            }
            else
            {
                Debug.LogError("No Level System !");
            }
        }
        private void SubOnLevelChanged(object sender, EventArgs e)
        {
            SetExperience(levelSystem.ExperienceNormalized);
            SetText();
        }
        private void SubOnMaxLevelChanged(object sender, System.EventArgs e)
        {
            SetExperience(levelSystem.ExperienceNormalized);
            SetText();
        }
        private void SubOnExperienceChanged(object sender, System.EventArgs e)
        {
            SetExperience(levelSystem.ExperienceNormalized);
            SetText();
        }
        public void SetExperience(float experienceNormalized)
        {
            fill.color = gradient.Evaluate(experienceNormalized);

            slider.value = experienceNormalized;

            SetText();
        }
        private void SetText()
        {
            barText.text = $"Level: {levelSystem.Level} - {levelSystem.Experience}/{levelSystem.ExperienceToNextLevel}";
        }
        public void SetLevelSystem(LevelSystem ls)
        {
            levelSystem = ls;
        }
        private void Setup()
        {
            posX = transform.GetComponent<RectTransform>().position.x;
            posY = transform.GetComponent<RectTransform>().position.y;
            posZ = transform.GetComponent<RectTransform>().position.z;
            slider = transform.GetComponent<Slider>();

            BackgroundSetup();

            BarSetup();

            TextSetup();

        }
        private void BackgroundSetup()
        {
            background = transform.Find("background");
            RectTransform backgroundRect = background.GetComponent<RectTransform>();
            Image backgroundImage = background.GetComponent<Image>();
            Outline backgroundOutline = background.GetComponent<Outline>();

            backgroundRect.position = new Vector3(posX, posY, posZ);
            backgroundRect.anchorMin = new Vector2(0f, 0f);
            backgroundRect.anchorMax = new Vector2(1f, 1f);
            backgroundRect.pivot = new Vector2(0.5f, 0.5f);

            backgroundImage.color = Color.gray;
            backgroundOutline.effectColor = Color.black;
            backgroundOutline.effectDistance = new Vector2(4, 4);

        }
        private void BarSetup()
        {
            bar = transform.Find("bar");
            fill = bar.GetComponent<Image>();

            RectTransform barRect = bar.GetComponent<RectTransform>();
            barRect.anchorMin = new Vector2(0.5f, 0.5f);
            barRect.anchorMax = new Vector2(0.5f, 0.5f);
            barRect.pivot = new Vector2(0.5f, 0.5f);


            fill.color = gradient.Evaluate(levelSystem.ExperienceNormalized);
            slider.value = levelSystem.ExperienceNormalized;
        }
        private void TextSetup()
        {
            text = transform.Find("text");
            barText = text.GetComponent<TextMeshProUGUI>();

            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.position = new Vector3(posX, posY, posZ);
            textRect.anchorMin = new Vector2(0f, 0f);
            textRect.anchorMax = new Vector2(1f, 1f);
            textRect.pivot = new Vector2(0.5f, 0.5f);

            SetText();
            barText.textStyle = TMP_Style.NormalStyle;
            barText.font = Resources.Load<TMP_FontAsset>("Fonts/LiberationSans SDF");
            barText.font.material = Resources.Load<Material>("Fonts/LiberationSans SDF - Outline");
            barText.fontStyle = TMPro.FontStyles.Bold;
            barText.fontSize = 18f;
            barText.faceColor = Color.white;
            barText.outlineWidth = .3f;
            barText.outlineColor = Color.black;
            barText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            barText.verticalAlignment = VerticalAlignmentOptions.Middle;
        }
    }
}