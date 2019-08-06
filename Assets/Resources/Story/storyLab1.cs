﻿/*
------------------------------------------------
Generated by Cradle 2.0.2.0
https://github.com/daterre/Cradle

Original file: storyLab1.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @storyLab1: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("firstOption", () => this.@firstOption, val => this.@firstOption = val);
			VarDef("secondOption", () => this.@secondOption, val => this.@secondOption = val);
			VarDef("thirdOption", () => this.@thirdOption, val => this.@thirdOption = val);
		}

		public StoryVar @firstOption;
		public StoryVar @secondOption;
		public StoryVar @thirdOption;
	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;

	@storyLab1()
	{
		this.StartPassage = "PreviousDie";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
		passage6_Init();
		passage7_Init();
		passage8_Init();
		passage9_Init();
		passage10_Init();
		passage11_Init();
		passage12_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: start

	void passage1_Init()
	{
		this.Passages[@"start"] = new StoryPassage(@"start", new string[]{  }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		yield return text("Сегодня я проснулся в отличном настроении. Дженни вчера меня приятно удивила, да " +
		    "и на работе проект подходит к концу. ");
		yield return lineBreak();
		yield return link("Полежать ещё " + 10 + " " + "мин", "sleep", null);
		yield return lineBreak();
		yield return link("Пойти умываться", "bath", null);
		yield return lineBreak();
		Vars.firstOption = 15;
		yield return lineBreak();
		Vars.secondOption = 15;
		yield return lineBreak();
		Vars.thirdOption = 69;
		yield break;
	}


	// .............
	// #2: PreviousDie

	void passage2_Init()
	{
		this.Passages[@"PreviousDie"] = new StoryPassage(@"PreviousDie", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("Ваше тело будет уничтожено в течение пяти секунд");
		yield return lineBreak();
		yield return link("Начать новую игру", "NewGame", null);
		yield return text("\t");
		yield return lineBreak();
		Vars.firstOption = 0;
		yield return lineBreak();
		Vars.secondOption = 0;
		yield return lineBreak();
		Vars.thirdOption = 0;
		yield break;
	}


	// .............
	// #3: NewGame

	void passage3_Init()
	{
		this.Passages[@"NewGame"] = new StoryPassage(@"NewGame", new string[]{  }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text("Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
		    "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру. Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру.  Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру. Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру. Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру. Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру. Привет, путешественник! Добро пожаловать в исследовательский центр корпорации \"Пр" +
            "обиус\". Двери в комнате надежно заперты и чтобы выбраться вам необходимо пройти " +
            "игру.");
		yield return lineBreak();
		yield return link("Далее", "PreStory", null);
		yield break;
	}


	// .............
	// #4: PreStory

	void passage4_Init()
	{
		this.Passages[@"PreStory"] = new StoryPassage(@"PreStory", new string[]{  }, passage4_Main);
	}

	IStoryThread passage4_Main()
	{
		yield return text("В данное время количество персонала в лаборатории: 0");
		yield return lineBreak();
		yield return link("Далее", "MoneyTutor", null);
		yield break;
	}


	// .............
	// #5: MoneyTutor

	void passage5_Init()
	{
		this.Passages[@"MoneyTutor"] = new StoryPassage(@"MoneyTutor", new string[]{  }, passage5_Main);
	}

	IStoryThread passage5_Main()
	{
		yield return text("Чтобы играть тебе будут необходимы монеты. Заработать их ты можешь, потратив само" +
		    "е дорогое, что у тебя есть");
		yield return lineBreak();
		yield return link("Понятно", "start", null);
		yield return lineBreak();
		Vars.firstOption = 10;
		yield break;
	}


	// .............
	// #6: sleep

	void passage6_Init()
	{
		this.Passages[@"sleep"] = new StoryPassage(@"sleep", new string[]{  }, passage6_Main);
	}

	IStoryThread passage6_Main()
	{
		yield return text("Вдруг заорала сирена!");
		yield return lineBreak();
		yield return link("Побежать на работу", "goToJob", null);
		yield return lineBreak();
		yield return link("Остаться дома", "stayAtHome", null);
		yield return lineBreak();
		Vars.firstOption = 20;
		yield return lineBreak();
		Vars.secondOption = 50;
		yield break;
	}


	// .............
	// #7: bath

	void passage7_Init()
	{
		this.Passages[@"bath"] = new StoryPassage(@"bath", new string[]{  }, passage7_Main);
	}

	IStoryThread passage7_Main()
	{
		yield return text("Как только я закончил приводит себя в порядок вдруг заорала сирена. Только не сег" +
		    "одня!");
		yield return lineBreak();
		yield return link("Отправиться на работу", "goToJob", null);
		yield return lineBreak();
		yield return link("Остаться дома", "stayAtHome", null);
		yield return lineBreak();
		Vars.firstOption = 20;
		yield return lineBreak();
		Vars.secondOption = 50;
		yield break;
	}


	// .............
	// #8: goToJob

	void passage8_Init()
	{
		this.Passages[@"goToJob"] = new StoryPassage(@"goToJob", new string[]{  }, passage8_Main);
	}

	IStoryThread passage8_Main()
	{
		yield return text("Прибежав в свой корпус, я сразу понял: \"Случилось что то неладное\". Вокруг была б" +
		    "ешанная суматоха, все носились как угарелые");
		yield return lineBreak();
		yield return link("Сесть на бутылку", "bottle", null);
		yield return lineBreak();
		yield return link("Сесть за рабочее место", "sitToJob", null);
		yield return lineBreak();
		Vars.firstOption = 100;
		yield return lineBreak();
		Vars.secondOption = 50;
		yield break;
	}


	// .............
	// #9: stayAtHome

	void passage9_Init()
	{
		this.Passages[@"stayAtHome"] = new StoryPassage(@"stayAtHome", new string[]{  }, passage9_Main);
	}

	IStoryThread passage9_Main()
	{
		yield return text("Через некоторое время за стенами стали слышны крики и мольбы о помощи. Но я не пр" +
		    "и чём, я словил дзен. Дышать становится трудно, но мне и не надо дышать.");
		yield return lineBreak();
		yield return link("Задохнуться", "die", null);
		yield return lineBreak();
		Vars.firstOption = 0;
		yield break;
	}


	// .............
	// #10: die

	void passage10_Init()
	{
		this.Passages[@"die"] = new StoryPassage(@"die", new string[]{  }, passage10_Main);
	}

	IStoryThread passage10_Main()
	{
		yield return text("Это был плохой вариант, попробуй ещё раз");
		yield return lineBreak();
		yield return link("Начать новую игру", "start", null);
		yield break;
	}


	// .............
	// #11: bottle

	void passage11_Init()
	{
		this.Passages[@"bottle"] = new StoryPassage(@"bottle", new string[]{  }, passage11_Main);
	}

	IStoryThread passage11_Main()
	{
		yield return text("Я присел на бутылку и мне стало хорошо. Если вам интересно, что будет дальше, ска" +
		    "жите разработчику чтобы шёл нахуй");
		yield break;
	}


	// .............
	// #12: sitToJob

	void passage12_Init()
	{
		this.Passages[@"sitToJob"] = new StoryPassage(@"sitToJob", new string[]{  }, passage12_Main);
	}

	IStoryThread passage12_Main()
	{
		yield return text("Если вам интересно, что будет дальше, скажите разработчику что хотите от него дет" +
		    "ей");
		yield break;
	}


}