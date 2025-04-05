
using System;
using System.Collections.Generic;
using tp1;
using tp2;

namespace tpfinal
{

	class Estrategia
	{

		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			string res = "";

			recorrerArbolHojas(arbol, ref res);

			return res;
			
			
		}

		private void recorrerArbolHojas(ArbolBinario<DecisionData> arbol , ref string resulto)
		{
            if (arbol != null)
            {
                if (arbol.esHoja())
                {
					resulto = $"{resulto} {arbol.getDatoRaiz().ToString()} \n";
                }

                recorrerArbolHojas(arbol.getHijoIzquierdo(),ref resulto);

                recorrerArbolHojas(arbol.getHijoDerecho(), ref resulto);

            }
        }


		public String Consulta2(ArbolBinario<DecisionData> arbol)
		{
            string res = "";

            recorrerArbolNoHojas(arbol, ref res);

            return res;

        }

        private void recorrerArbolNoHojas(ArbolBinario<DecisionData> arbol, ref string resulto)
        {
            if (arbol != null)
            {
                if (!arbol.esHoja())
                {
                    resulto = $"{resulto} {arbol.getDatoRaiz().ToString()} \n";
                }

                recorrerArbolNoHojas(arbol.getHijoIzquierdo(), ref resulto);

                recorrerArbolNoHojas(arbol.getHijoDerecho(), ref resulto);

            }
        }


        public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
            int nivel = 0;
            String result = "";
            Queue<ArbolBinario<DecisionData>> Cola = new Queue<ArbolBinario<DecisionData>>();
            ArbolBinario<DecisionData> aux;
            Cola.Enqueue(arbol);
            Cola.Enqueue(null);
            while (Cola.Count != 0)
            {
                aux = Cola.Dequeue();

                if (aux != null)
                {
                    result = result + nivel + aux.getDatoRaiz().ToString() + ", ";
                    if (aux.getHijoDerecho() != null)
                    {
                        Cola.Enqueue(aux.getHijoDerecho());
                    }
                    if (aux.getHijoIzquierdo() != null)
                    {
                        Cola.Enqueue(aux.getHijoIzquierdo());
                    }
                }
                else
                {
                    if (Cola.Count != 0)
                    {
                        Cola.Enqueue(null);


                    }
                    ++nivel;
                    result += "\n";
                }


            }

            return result;
        }

		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{

            if (clasificador.crearHoja())
            {
				return new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerDatoHoja()));
            }
            else
            {
                var res = new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerPregunta()));
				res.agregarHijoDerecho(CrearArbol(clasificador.obtenerClasificadorDerecho()));
				res.agregarHijoIzquierdo(CrearArbol(clasificador.obtenerClasificadorIzquierdo()));

				return res;
            }
        }
	}
}