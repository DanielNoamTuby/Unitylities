using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Unitylities
{
    public class ShieldBar : MonoBehaviour
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
        private HealthSystem healthSystem;
        public Gradient gradient;

        private void Awake()
        {
            Setup();
        }
        private void Start()
        {
            if (healthSystem != null)
            {
                SetShield(healthSystem.ShieldNormalized);

                healthSystem.OnDamaged += SubOnDamaged;
                healthSystem.OnShieldBroke += SubOnShieldBroke;
                healthSystem.OnShieldChanged += SubOnShieldChanged;
                healthSystem.OnFullShield += SubOnFullShield;
            }
            else
            {
                Debug.LogError("No Health System !");
            }
        }

        private void SubOnShieldBroke(object sender, EventArgs e)
        {
            Debug.Log("Shield Broke !");
        }
        private void SubOnFullShield(object sender, System.EventArgs e)
        {
            SetShield(healthSystem.Shield);
        }
        private void SubOnShieldChanged(object sender, System.EventArgs e)
        {
            SetShield(healthSystem.ShieldNormalized);
            SetText();
        }
        private void SubOnDamaged(object sender, System.EventArgs e)
        {
            SetShield(healthSystem.ShieldNormalized);
        }
        public void SetShield(float shieldNormalized)
        {
            fill.color = gradient.Evaluate(shieldNormalized);

            slider.value = shieldNormalized;

            SetText();
        }
        private void SetText()
        {
            barText.text = $"{healthSystem.Shield}/{healthSystem.MaxShield}";
        }
        public void SetHealthSystem(HealthSystem hs)
        {
            healthSystem = hs;
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
            barRect.position = new Vector3(posX, posY, posZ);
            barRect.anchorMin = new Vector2(0f, 0f);
            barRect.anchorMax = new Vector2(1f, 1f);
            barRect.pivot = new Vector2(0.5f, 0.5f);

            fill.color = gradient.Evaluate(healthSystem.ShieldNormalized);
            slider.value = healthSystem.ShieldNormalized;
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