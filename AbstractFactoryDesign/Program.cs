using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class Program
    {
        /*        
        Abstract Factory 
        
        Es un patrón de creación que proporciona una interfaz 
        para crear familias de objetos relacionados o dependientes sin especificar sus clases concretas. 
        
        El patrón se divide en dos partes: 
        
        *   La fábrica abstracta 
        *   Las fábricas concretas

        La fábrica abstracta es una interfaz o una clase abstracta que define los métodos para crear los objetos de una familia. 
        Las fábricas concretas son clases que implementan los métodos de la fábrica abstracta para crear objetos específicos. */
        static void Main(string[] args)
        {
            /*
            Ejemplo:
                Puede ser la creación de objetos de un juego de rol. 
                Podemos tener diferentes tipos de personajes (guerrero, mago, ladrón) y armas (espada, arco, varita) 
                pero todos ellos deben ser compatibles entre sí. 
                En lugar de crear objetos específicos de cada tipo de personaje y arma, 
                podemos utilizar una fábrica abstracta para crear familias de objetos compatibles.

                En este ejemplo se define una interfaz ICharacter y IWeapon, 
                se crean dos clases concretas que implementan cada interfaz (Warrior y Mage para ICharacter, Sword y Wand para IWeapon), 
                se define una interfaz IAbstractFactory que es implementada por WarriorFactory y MageFactory. 
                En la clase Game, se utilizan las fábricas para crear personajes y armas compatibles.
             */

            var game = new Juego();
                game.CrearMago();
                game.CrearGuerrero();
                game.CrearOdiseo();

            Console.ReadLine();
        }
        public interface IPersonaje
        {
            void Atacar();
        }
        public interface IArma
        {
            void Utilizar();
        }
        public interface IAbstractFactory
        {
            IPersonaje crearPersonaje();
            IArma crearArma();
        }
        public class Odiseo : IPersonaje
        {
            public void Atacar()
            {
                Console.WriteLine("Odiseo Ataco.");
            }
        }
        public class Guerrero : IPersonaje
        {
            public void Atacar()
            {
                Console.WriteLine("Guerrero ataco.");
            }
        }
        public class Mago : IPersonaje
        {
            public void Atacar()
            {
                Console.WriteLine("Mago ataco");
            }
        }
        public class Espada : IArma
        {
            public void Utilizar()
            {
                Console.WriteLine($"Utiliza Espada");
            }
        }
        public class Magia : IArma
        {
            public void Utilizar()
            {
                Console.WriteLine($"Utiliza Magia");
            }
        }
        public class Tridente : IArma
        {
            public void Utilizar()
            {
                Console.WriteLine("Utiliza Tridente");
            }
        }
        public class GuerreroFactory : IAbstractFactory
        {
            public IPersonaje crearPersonaje() => new Guerrero();
            public IArma crearArma() => new Espada();
        }
        public class MagoFactory : IAbstractFactory
        {
            public IPersonaje crearPersonaje() => new Mago();
            public IArma crearArma() => new Magia();
        }
        public class OdiseoFactory : IAbstractFactory
        {
            public IArma crearArma() => new Tridente();
            public IPersonaje crearPersonaje() => new Odiseo();
        }
        public class Juego
        {
            public void CrearGuerrero()
            {
                IAbstractFactory factoria = new GuerreroFactory();
                IPersonaje personaje = factoria.crearPersonaje();
                IArma arma = factoria.crearArma();
                personaje.Atacar();
                arma.Utilizar();
            }
            public void CrearMago()
            {
                IAbstractFactory factoria = new MagoFactory();
                IPersonaje personaje = factoria.crearPersonaje();
                IArma arma = factoria.crearArma();
                personaje.Atacar();
                arma.Utilizar();
            }
            public void CrearOdiseo()
            {
                var factoria = new OdiseoFactory();
                var personaje = factoria.crearPersonaje();
                var arma = factoria.crearArma();
                personaje.Atacar();
                arma.Utilizar();
            }
        }
    }
}
