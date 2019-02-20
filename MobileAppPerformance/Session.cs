//
// Copyright (c) 2015 Harry Cheung
//

using System.Collections.Generic;

namespace MobileAppPerformance
{
	public class Session
	{
		public Track track;
		public double startTime;
		public double duration = 0;
		public List<Lap> laps = new List<Lap>();

		public Session(Track track, double startTime) {
			this.track     = track;
			this.startTime = startTime;
		}
	}
}

