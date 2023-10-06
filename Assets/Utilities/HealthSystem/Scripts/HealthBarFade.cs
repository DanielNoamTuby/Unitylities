using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unitylities;

namespace Unitylities
{
    public class HealthBarFade : MonoBehaviour
    {
        private const float DAMAGED_HEALTH_FADE_TIMER_MAX = .6f;

        private float width;
        private float height;
        private float posX;
        private float posY;
        private float posZ;
        private float damagedHealthFadeTimer;

        private Image fill;
        private Image damagedFill;
        private Color damagedColor;
        private Transform bar;
        private Transform background;
        private Transform damagedBar;
        private HealthSystem healthSystem;


        private void Awake()
        {
            Setup();
        }
        private void Start()
        {
            if (healthSystem != null)
            {
                SetHealth(healthSystem.HealthNormalized);

                healthSystem.OnDamaged += SubOnDamaged;
                healthSystem.OnHeal += SubOnHeal;
            }
            else
            {
                Debug.LogError("No Health System !");
            }
        }
        private void Update()
        {
            if (damagedColor.a > 0)
            {
                damagedHealthFadeTimer -= Time.deltaTime;
                if (damagedHealthFadeTimer < 0)
                {
                    float fadeAmount = 5f;
                    damagedColor.a -= fadeAmount * Time.deltaTime;
                    damagedFill.color = damagedColor;
                }
            }
        }
        private void SubOnHeal(object sender, System.EventArgs e)
        {
            SetHealth(healthSystem.HealthNormalized);
        }
        private void SubOnDamaged(object sender, System.EventArgs e)
        {
            if (damagedColor.a <= 0)
            {
                // Damaged bar image is invisible
                damagedFill.fillAmount = fill.fillAmount;
            }
            damagedColor.a = 1;
            damagedFill.color = damagedColor;
            damagedHealthFadeTimer = DAMAGED_HEALTH_FADE_TIMER_MAX;

            SetHealth(healthSystem.HealthNormalized);
        }
        private void SetHealth(float healthNormalized)
        {
            fill.fillAmount = healthNormalized;
        }
        public void SetHealthSystem(HealthSystem hs)
        {
            healthSystem = hs;
        }
        private void Setup()
        {
            width = transform.GetComponent<RectTransform>().rect.width;
            height = transform.GetComponent<RectTransform>().rect.height;
            posX = transform.GetComponent<RectTransform>().position.x;
            posY = transform.GetComponent<RectTransform>().position.y;
            posZ = transform.GetComponent<RectTransform>().position.z;

            BackgroundSetup();

            BarSetup();

            DamagedBarSetup();
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
            Sprite sprite = Resources.Load<Sprite>("HealthBarGradient");

            RectTransform barRect = bar.GetComponent<RectTransform>();
            barRect.position = new Vector3(posX, posY, posZ);
            barRect.anchorMin = new Vector2(0f, 0f);
            barRect.anchorMax = new Vector2(1f, 1f);
            barRect.pivot = new Vector2(0.5f, 0.5f);

            if (sprite != null)
            {
                Debug.Log("Sprite should load !");
                fill.sprite = sprite;
                fill.type = Image.Type.Filled;
                fill.fillMethod = Image.FillMethod.Horizontal;
                fill.fillOrigin = (int)Image.OriginHorizontal.Left;
            }
            else
            {
                Debug.LogError("Can't find the health bar sprite...");
            }
        }
        private void DamagedBarSetup()
        {
            damagedBar = transform.Find("damagedBar");
            damagedFill = damagedBar.GetComponent<Image>();
            Sprite sprite = Resources.Load<Sprite>("White_1x1");

            damagedColor = damagedFill.color;
            damagedColor.a = 0f;
            damagedFill.color = damagedColor;

            RectTransform damagedBarRect = damagedBar.GetComponent<RectTransform>();
            damagedBarRect.position = new Vector3(posX, posY, posZ);
            damagedBarRect.anchorMin = new Vector2(0.5f, 0.5f);
            damagedBarRect.anchorMax = new Vector2(0.5f, 0.5f);
            damagedBarRect.pivot = new Vector2(0.5f, 0.5f);
            damagedBarRect.sizeDelta = new Vector2(width, height);

            if (sprite != null)
            {
                Debug.Log("Sprite should load !");
                damagedFill.sprite = sprite;
                damagedFill.type = Image.Type.Filled;
                damagedFill.fillMethod = Image.FillMethod.Horizontal;
                damagedFill.fillOrigin = (int)Image.OriginHorizontal.Left;
            }
            else
            {
                Debug.LogError("Can't find the health bar sprite...");
            }
        }
    }
}