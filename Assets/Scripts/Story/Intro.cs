using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

namespace Assets.Scripts.Story
{
    public class Intro : MonoBehaviour
    {

        [SerializeField] private Text introText;
        [SerializeField] private StoryController storyController;

        private Image introScreen;
        void Awake()
        {
            introText.text = "";
            introScreen = GetComponent<Image>();
            if (PlayerPrefs.GetInt("continue_game").Equals(0))
            {
                StartCoroutine(TextPrint(introText, "Вы - молодой человек, странствующий по миру в поисках приключений.\nОднажды, изучая бескрайние просторы Арктики вы набрели на заброшенную лабораторию.\nТолько стоило Вам войти внутрь, как дверь за вами наглухо захлопнулась и вы оказались в небольшой пустой комнате. Единстенная вещь - это компьютер в центре комнаты.\nНе зная страха, вы включили его... \n\nНажмите на экран для продолжения ", 0.1f));
            }
            else
            {
                HideIntroScreen();
            }
        }

        private void HideIntroScreen()
        {
            PlayerPrefs.SetInt("continue_game",1);
            StartCoroutine(HideIntroByAlpha());
        }

        IEnumerator TextPrint(Text output, string input, float delay)
        {
            //вывод текста побуквенно
            for (int i = 0; i <= input.Length; i++)
            {
                output.text = input.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }

            Button button = GetComponent<Button>();
            button.onClick.AddListener(HideIntroScreen);
        }

        IEnumerator HideIntroByAlpha()
        {
            float nextAlpha = 1;
            while (nextAlpha > 0)
            {
                nextAlpha = introScreen.color.a - 0.05f;
                introScreen.color = new Color(introScreen.color.r, introScreen.color.g, introScreen.color.b, nextAlpha);
                yield return null;
            }
            storyController.StartStory(); 
            gameObject.SetActive(false);
        }
    }
}
