using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Extensions {

    public static bool AtEndOfAnimation(this Animator animator) {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !animator.IsInTransition(0)) {
            return false;
        }
        return true;
    }

    public static void RespectHeight(this Transform t, float height) {
        t.position = new Vector3(t.position.x, height, t.position.z);
    }

    public static void SafePlay(this Animator animator, string a, int layer = 0, float time = 0) {
        if (!animator.gameObject.activeSelf) {
            Debug.Log("WARNING animator is disabled, trying to play: " + a);
            return;
        }

        var state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName(a)) {
            return;
        }
      
        animator.Play(a, layer, time);
    }

    public static T Closest<T>(this List<T> objects, Vector3 p) where T : MonoBehaviour {
        float distance = Vector3.Distance(p, objects[0].transform.position);
        T closest = objects[0];
        for (int i = 1; i < objects.Count; i++) {
            T testClosest = objects[i];
            float testDistance = Vector3.Distance(testClosest.transform.position, p);
            if (testDistance < distance) {
                closest = testClosest;
                distance = testDistance;
            }
        }
        return closest;
    }

    public static T Detect<T>(this Component detector,Vector3 centre, Vector3 extents) where T : Component {
        Collider[] blocks = Physics.OverlapBox(centre, extents / 2.0f);
        foreach (Collider c in blocks) {
            if (c == null) {
                continue;
            }
            T comp = c.GetComponent<T>();
            if (comp != null && comp.gameObject != detector.gameObject) {
                return comp;
            }
        }
        return null;
    }

    public static T Detect<T>(this Component detector, Vector3 extents) where T : Component {
        Collider[] blocks = Physics.OverlapBox(detector.transform.position, extents/2.0f);
        foreach (Collider c in blocks) {
            if (c == null) {
                continue;
            }
            T comp = c.GetComponent<T>();
            if (comp != null && comp.gameObject != detector.gameObject) {
                return comp;
            }
        }
        return null;
    }

    public static T Detect<T>(this Component detector, Vector3 extents, LayerMask mask) where T : Component {
        Collider[] blocks = Physics.OverlapBox(detector.transform.position, extents / 2.0f, detector.transform.localRotation, mask);
        foreach (Collider c in blocks) {
            if (c == null) {
                continue;
            }
            T comp = c.GetComponent<T>();
            if (comp != null && comp.gameObject != detector.gameObject) {
                return comp;
            }
        }
        return null;
    }

    public static void SafeEnable<T>(this T mono) where T : MonoBehaviour {
        if (mono != null && !mono.enabled) {
            mono.enabled = true;
        }
    }

    public static void SafeDisable<T>(this T mono) where T : MonoBehaviour {
        if (mono != null && mono.enabled) {
            mono.enabled = false;
        }
    }

    public static void SafeEnable(this GameObject g) {
        if (g != null && !g.activeSelf) {
            g.SetActive(true);
        }
    }

    public static void SafeDisable(this GameObject g) {
        if (g != null && g.activeSelf) {
            g.SetActive(false);
        }
    }


    public static T AddIfNull<T>(this GameObject g) where T : Component {
        if (g.GetComponent<T>() == null) {
            return g.AddComponent<T>();
        }
        return null;
    }


    static float epsilon = 0.012f;
	public static bool SameAs(this Color color, Color c){
		if(
			CompareFloats(color.r, c.r, epsilon) &&
			CompareFloats(color.g, c.g, epsilon) &&
			CompareFloats(color.b, c.b, epsilon)
			) {
			return true;
		}return false;
	}
	
	public static bool CompareFloats(float a, float b, float e){
		if (Mathf.Abs(a - b) <= e) {
			return true;
		} return false;
	}
	
	public static System.Random localRandom = new System.Random();
	
	public static T GetRandomElement<T>(this IEnumerable<T> list, System.Random random) {
		if (list.Count() == 0)
			return default(T);
		return list.ElementAt(random.Next(list.Count()));
	}
	
	public static string ToStringAll<T>(this T[] array) {
		if (array.Length == 0){
			return "empty";
		}
		string s = "";
		foreach(T n in array){
			s += n.ToString() + " | ";
		}
		return s;
	}
	
	public static List<T> Sort<T>(this List<T> list)  { 
		list.Sort();
		return list;
	}
	
	public static int IndexConstrain(this int index, int max) { 
		if(index > max){
			return 0;
		}
		else if (index < 0) {
			return max;
		}
		else {
			return index;
		}
	}
	
	public static T GetRandomElement<T>(this IEnumerable<T> list) {
		if (list.Count() == 0)
			return default(T);
		return list.ElementAt(localRandom.Next(list.Count()));
	}
	
	public static T GetRandomElementExcluding<T>(this IEnumerable<T> list, IEnumerable<T> excludes) {
		if (list.Count() == 0)
			return default(T);
		
		List<T> filtered = (from n in list
		                    where !(excludes.Contains(n))
		                    select n).ToList<T>();
		
		if (filtered.Count == 0) {
			return default(T);
		}else if (filtered.Count == 1) {
			return filtered[0];
		} return filtered.GetRandomElement<T>();
	}
	
	public static T GetRandomElement<T>(this T[] array) {
		if (array.Length == 0)
			return default(T);
		return array.ElementAt(localRandom.Next(array.Length));
	}
	
	public static T Chomp<T>(this List<T> list,T defaultTo) {
		
		if(list.Count == 0){
			return defaultTo;
		}
		
		T b = list[0];
		list.Remove(b);
		return b;
	}
	
	public static T Snatch<T>(this List<T> list, int index){
		T b = list[index];
		list.Remove(b);
		return b;
	}
	
	public static T Chomp<T>(this List<T> list) {
		T b = list[0];
		list.Remove(b);
		return b;
	}
	
	public static T ChompLast<T>(this List<T> list, T defaultTo) {
		
		if(list.Count == 0){
			return defaultTo;
		}
		
		T b = list[list.Count-1];
		list.RemoveAt(list.Count-1);
		return b;
	}
	
	public static T ChompUntil<T>(this List<T> list, T defaultTo, int size) {
		
		if(list.Count == 0){
			return defaultTo;
		}
		
		if(list.Count <=size){
			return list.Chomp();
		}
		
		if(list.Count == 0){
			return defaultTo;
		}
		
		while(list.Count > size+1){
			list.Chomp();
		}
		
		return list.Chomp();
	}
	
	public static T ChompRandom<T>(this List<T> list) {
		int rand = localRandom.Next(list.Count());
		T b = list[rand];
		list.RemoveAt(rand);
		return b;
	}
	
	
	public static List<T> Shuffle<T>(this List<T> list, System.Random rng) {
		int n = list.Count;
		while (n > 1) {
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
		return list;
	}

    public static Vector3 Round(this Vector3 vect) {
		return new Vector3(Mathf.RoundToInt(vect.x), Mathf.RoundToInt(vect.y), Mathf.RoundToInt(vect.z));
	}
	
	public static Vector3 CeilXToInt(this Vector3 vect){
		return new Vector3(Mathf.CeilToInt(vect.x),vect.y,vect.z);
	}
	
	public static Vector3 CeilZToInt(this Vector3 vect){
		return new Vector3(vect.x,vect.y,Mathf.CeilToInt(vect.z));
	}
	
	public static Vector3 RoundXToInt(this Vector3 vect){
		return new Vector3(Mathf.RoundToInt(vect.x),vect.y,vect.z);
	}
	
	public static Vector3 RoundZToInt(this Vector3 vect){
		return new Vector3(vect.x,vect.y,Mathf.RoundToInt(vect.z));
	}
	
	public static Vector3 CentreOfVerts(this MeshFilter filter) {
		Vector3 avg = new Vector3();
		foreach (Vector3 v in filter.mesh.vertices) {
			avg += v;
		}
		return avg / filter.mesh.vertexCount;
	}
	
	public static bool IsEven(this int i) {
		return (i % 2) == 0;
	}
	
	public static float MinComp(this Vector3 vect) {
		float toReturn = vect[0];
		for (int i = 1; i < 3; i++) {
			if (vect[i] < toReturn) {
				toReturn = vect[i];
			}
		}
		return toReturn;
	}
	
	public static T Duplicate<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Object {
		return (T)Object.Instantiate(prefab, position, rotation);
	}
	
	public static T Duplicate<T>(this T prefab, Vector3 position) where T : Object {
		return (T)Object.Instantiate(prefab, position, Quaternion.identity);
	}
	
	public static T Duplicate<T>(this T prefab) where T : Object {
		return prefab.Duplicate(Vector3.zero);
	}
	
	public static T Duplicate<T>(this T prefab, Transform transform) where T : Object {
		return prefab.Duplicate(transform.position, transform.rotation);
	}

	public static T GetFirst<T>(this Transform t, Vector3 e, LayerMask m) where T : Object {
		var things = Physics.OverlapBox(t.position, e/2.0f, Quaternion.identity, m);
		T x = null;
		for(int i = 0; i < things.Length; i++){
			var n = things[i];
			x = n.transform.GetComponent<T>();
			if(x != null){
				break;
			}
		}
		return x;
	}

}