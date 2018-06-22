using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class Birb
{

    abstract public class Component
    {
        abstract public Element Render(Dictionary<string, object> props);
    }

    public class Element
    {
        public Component component;
        public GameObject prefab;
        public Dictionary<string, object> props;

        public Element(Component component, Dictionary<string, object> props)
        {
            this.component = component;
            this.props = props;
        }

        public Element(GameObject prefab, Dictionary<string, object> props)
        {
            this.prefab = prefab;
            this.props = props;
        }
    }

    public static Element CreateElement(GameObject prefab, Dictionary<string, object> props)
    {
        return new Element(prefab, props);
    }

    public static Element CreateElement(Component component, Dictionary<string, object> props)
    {
        return new Element(component, props);
    }

    class BirbRoot : MonoBehaviour
    {
        public Element element;
        public BirbRenderer birbRenderer;

        public void Draw()
        {
            birbRenderer.Draw(element);
        }
    }

    public class Props : Dictionary<string, object> { }

    class PropsComponent : MonoBehaviour
    {
        public Dictionary<string, object> props;
    }

    class BirbRenderer : MonoBehaviour
    {
        public GameObject parent;
        GameObject instance;

        public void Draw(Element element)
        {
            this.instance = Instantiate(element.prefab);
            if (this.parent != null)
            {
                this.instance.transform.SetParent(this.parent.transform);
            }
            instance.SetActive(true);
            PropsComponent component = instance.AddComponent(
                typeof(PropsComponent)
            ) as PropsComponent;
            component.props = element.props;
        }
    }

    public static void Render(GameObject host, Element element)
    {
        BirbRoot root = host.AddComponent(typeof(BirbRoot)) as BirbRoot;
        BirbRenderer birbRenderer = host.AddComponent(
            typeof(BirbRenderer)
        ) as BirbRenderer;
        birbRenderer.parent = host;

        root.element = element;
        root.birbRenderer = birbRenderer;
        root.Draw();
    }

}
