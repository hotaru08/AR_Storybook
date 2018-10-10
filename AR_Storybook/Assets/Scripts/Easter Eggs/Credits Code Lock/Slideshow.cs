using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
	[SerializeField] List<Sprite> slides;
	[SerializeField] int slideshowFrequency;

	Image image;
	float timeBetweenSlides;

	private void Start()
	{
		timeBetweenSlides = 1f / slideshowFrequency;
		image = GetComponent<Image>();

		StartCoroutine("SlideShow", 0);
	}

	private IEnumerator SlideShow(int startIndex)
	{
		int index = startIndex;

		while(true)
		{
			image.preserveAspect = true;
			image.sprite = slides[index];

			index++;
			if (index > slides.Count - 1)
				index = 0;

			yield return new WaitForSeconds(timeBetweenSlides);
		}
	}
}
