using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class Birb {

	abstract public class Component<P> {
		abstract public Element<T> Render<T>(T props);
	}

	public class Element<P> {
		public Component<P> component;
		public GameObject prefab;
		public P props;

		public Element(Component<P> component, P props)
		{
			this.component = component;
			this.props = props;
		}

		public Element(GameObject prefab, P props)
		{
			this.prefab = prefab;
			this.props = props;
		}
	}

	public static Element<P> CreateElement<P>(GameObject prefab, P props)
	{
		return new Element<P>(prefab, props);
	}

	public static Element<P> CreateElement<P>(Component<P> component, P props)
	{
		return new Element<P>(component, props);
	}

	class BirbRoot : MonoBehaviour {
		public Element<object> element;
	}

	public static void Render(GameObject host, Element<object> element)
	{
		BirbRoot root = host.AddComponent(typeof(BirbRoot)) as BirbRoot;
		root.element = element;
	}
}
