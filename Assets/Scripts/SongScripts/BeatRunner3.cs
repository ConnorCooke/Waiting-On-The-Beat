using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRunner3 : BeatRunner
{
    // Start is called before the first frame update

	protected override void Start()
    {
		beatFilePath = "/Resources/Songs/Serve.txt";
        base.Start();
		

    }
}
