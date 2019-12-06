using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRunner2 : BeatRunner
{
    // Start is called before the first frame update

	protected override void Start()
    {
		beatFilePath = "/Resources/Songs/Pavana.txt";
        base.Start();
		

    }
}
