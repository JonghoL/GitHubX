using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin;

namespace GitHubX.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			Insights.Initialize("ea03e6943fc77195b07ef410914977731faea368");
			Insights.ForceDataTransmission = true;
			Insights.Identify ("mail@jongholee.kr", "Name", "jongho");

			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

