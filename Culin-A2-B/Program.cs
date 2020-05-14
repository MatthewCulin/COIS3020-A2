/*
|   
|   FILE TYPE:      C# PROGRAM
|   AUTHOR:         MATTHEW CULIN
|   DATE:           FEBRUARY 2020
|       
|   PURPOSE:        PROGRAM TO CREATE ROPES
|   USEAGE:         Culin-A2-B.exe
|
|   CLASSES:
|
|           ROPE
|       ------------
|           PUBLIC METHODS: 
|               Rope(string S)
|               Insert(string S, int i)
|               Delete(int i, int j)
|               Substring(int i, int j)
|               CharAt(int i)
|               Split(int i)
|               Concatenate(Rope r1, Rope r2)
|               Print()
|
|           PRIVATE METHODS: 
|               GetRopeString(Node curr, string ropeString)
|
|           NODE
|       ------------
|           DATA MEMBERS:
|               public char Type
|               public int SubLength
|               public Node Left
|               public Node Right
|               public char[] SubString
|
|           PUBLIC METHODS: 
|               Node()
|               Node(char[] subString)
|
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Culin_A2_B
{
    /*----------------------------------------------------------------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |------------------------------------------- PROGRAM CLASS -------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |----------------------------------------------------------------------------------------------------*/
    class Program
    {
        static void Main()
        {
            // READ IN A FILE 
            // USE AS TEXT INPUT FOR ROPE
            string fileName = @"input.txt";
            string input = File.ReadAllText(fileName);

            /* ------------------
            |       ROPE 1 
            |------------------*/
            Console.WriteLine("\n\t------------------------------------------------------");
            Console.WriteLine("\t----------------------- ROPE 1 -----------------------");
            Console.WriteLine("\t------------------------------------------------------");

            Rope r1 = new Rope("I_love_both_my_cats_");

            // ***********
            // ** PRINT **
            // ***********
            Console.WriteLine("\n-----------------");
            Console.WriteLine("----- PRINT -----");
            Console.WriteLine("-----------------");
            r1.Print();
            Console.ReadKey();

            // *************
            // ** CHAR AT **
            // *************
            Console.WriteLine("\n-------------------");
            Console.WriteLine("----- CHAR AT -----");
            Console.WriteLine("-------------------");
            Console.WriteLine("\t {0}", r1.CharAt(7));
            Console.ReadKey();


            /* ------------------
            |       ROPE 2 
            |------------------*/
            Console.WriteLine("\n\t------------------------------------------------------");
            Console.WriteLine("\t----------------------- ROPE 2 -----------------------");
            Console.WriteLine("\t------------------------------------------------------");

            Rope r2 = new Rope("because_they_are_so_fluffy");

            // ***********
            // ** PRINT **
            // ***********
            Console.WriteLine("\n-----------------");
            Console.WriteLine("----- PRINT -----");
            Console.WriteLine("-----------------");
            r2.Print();
            Console.ReadKey();

            // ***************
            // ** SUBSTRING **
            // ***************
            Console.WriteLine("\n---------------------");
            Console.WriteLine("----- SUBSTRING -----");
            Console.WriteLine("---------------------");
            // FIRST LEAF
            Console.WriteLine("\t {0}", r2.Substring(2, 4));
            Console.ReadKey();

            // FIRST --> SECOND LEAF
            Console.WriteLine("\t {0}", r2.Substring(3, 8));
            Console.ReadKey();
            
            // SECOND LEAF
            Console.WriteLine("\t {0}", r2.Substring(8, 12));
            Console.ReadKey();

            // SECOND --> THIRD LEAF
            Console.WriteLine("\t {0}", r2.Substring(11, 17));
            Console.ReadKey();

            // THIRD LEAF
            Console.WriteLine("\t {0}", r2.Substring(13, 15));
            Console.ReadKey();

            // THIRD --> FOURTH LEAF
            Console.WriteLine("\t {0}", r2.Substring(17, 23));
            Console.ReadKey();

            // FIRST --> FOURTH LEAF
            Console.WriteLine("\t {0}", r2.Substring(3, 23));
            Console.ReadKey();
            
            // NOT IN ROPE
            Console.WriteLine("\t {0}", r2.Substring(3, 27));
            Console.ReadKey();
            
            // SINGLE CHARACTER
            Console.WriteLine("\t {0}", r2.Substring(3, 3));
            Console.ReadKey();


            /* ------------------
            |       ROPE 3 
            |------------------*/
            Console.WriteLine("\n\t------------------------------------------------------");
            Console.WriteLine("\t----------------------- ROPE 3 -----------------------");
            Console.WriteLine("\t------------------------------------------------------");

            Rope r3 = new Rope("ROPE_3");

            // *****************
            // ** CONCATENATE **
            // *****************
            Console.WriteLine("\n-----------------------");
            Console.WriteLine("----- CONCATENATE -----");
            Console.WriteLine("-----------------------");
            r3.root.Left = r3.Concatenate(r1.root.Left, r2.root.Left);
            r3.root.Left.SubLength = r1.root.SubLength;
            r3.root.SubLength = r3.root.Left.SubLength + r2.root.SubLength;
            Console.WriteLine("\n... CONCATENATE R1 AND R2 ...");
            r3.Print();
            Console.ReadKey();

            // ************
            // ** INSERT **
            // ************
            Console.WriteLine("\n------------------");
            Console.WriteLine("----- INSERT -----");
            Console.WriteLine("------------------");
            if (r3.Insert("AND_MY_DOG_", 20))
            {
                Console.WriteLine("\n... INSERTED 'AND_MY_DOG_' INTO ROPE 3 ...");
                r3.Print();
            }
            else
                Console.WriteLine("\n... INSERT FAILED ...");
            Console.ReadKey();

            // ***************
            // ** REBALANCE **
            // ***************
            r3.ReBalance();

            // ************
            // ** DELETE **
            // ************
            Console.WriteLine("\n------------------");
            Console.WriteLine("----- DELETE -----");
            Console.WriteLine("------------------");
            if (r3.Delete(30, 43))
            {
                Console.WriteLine("\n... DELETED A SUBSTRING FROM ROPE 3 ...");
                r3.Print();
            }
            else
                Console.WriteLine("\n... DELETE FAILED ...");
            Console.ReadKey();

            /* ------------------
            |       ROPE 4 
            |------------------*/

            Console.WriteLine("\n\t------------------------------------------------------");
            Console.WriteLine("\t----------------------- ROPE 4 -----------------------");
            Console.WriteLine("\t------------------------------------------------------");

            // ROPE CREATED WITH INPUT FROM A TEXT FILE 
            // 3910 CHARACTERS
            Rope r4 = new Rope(input);

            // ***********
            // ** PRINT **
            // ***********
            Console.WriteLine("\n-----------------");
            Console.WriteLine("----- PRINT -----");
            Console.WriteLine("-----------------");
            r4.Print();
            Console.ReadKey();

            // ***********
            // ** SPLIT **
            // ***********
            Console.WriteLine("\n-----------------");
            Console.WriteLine("----- SPLIT -----");
            Console.WriteLine("-----------------");
            r4.Split(457);
            Console.ReadKey();

            // ***************
            // ** REBALANCE **
            // ***************
            r4.ReBalance();

            // ************
            // ** INSERT **
            // ************
            Console.WriteLine("\n------------------");
            Console.WriteLine("----- INSERT -----");
            Console.WriteLine("------------------");
            if (r4.Insert("   -----Procuring education on consulted assurance in do-----   ", 234))
            {
                Console.WriteLine("\n... INSERTED '   -----Procuring education on consulted assurance in do-----  ' INTO ROPE 3 ...");
                r4.Print();
            }
            else
                Console.WriteLine("\n... INSERT FAILED ...");
            Console.ReadKey();
            Console.WriteLine();
        }// END OF MAIN
    }// END OF PROGRAM CLASS




    /*----------------------------------------------------------------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |-------------------------------------------- ROPE CLASS ---------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |----------------------------------------------------------------------------------------------------*/
    public class Rope
    {
        /*----------------------------------------------------------------------------------------------------|
        |-----------------------------------------------------------------------------------------------------|
        |-------------------------------------------- NODE CLASS ---------------------------------------------|
        |-----------------------------------------------------------------------------------------------------|
        |----------------------------------------------------------------------------------------------------*/
        public class Node
        {
            public int SubLength { get; set; }      // LENGTH OF THE SUBSTRING STORED TO THE LEFT

            public Node Left { get; set; }          // LEFT NODE

            public Node Right { get; set; }         // RIGHT NODE

            public string SubString { get; set; }   // SUBSTRING


            /*------------------------------------------------
            |
            |   Name: Node
            |
            |   Description: Node constructor (creates a root)
            |   
            ------------------------------------------------*/
            public Node()
            {
                Left = null;
                Right = null;
                SubLength = 0;
                SubString = "";
            }// END OF NODE CONSTRUCTOR


        }// END OF NODE CLASS

        // ROOT NODE OF ROPE
        public Node root = new Node();

        /*------------------------------------------------
        |
        |   Name: Rope
        |
        |   Description: Rope constructor
        |   
        ------------------------------------------------*/
        public Rope(string S)
        {
            Node temp = new Node();
            root.Left = temp;
            root.SubLength = S.Length;

            // ASSIGN STRING TO TEMP
            if (S.Length <= 10)
            {
                temp.SubLength = S.Length;
                temp.SubString = S;
            }

            else
            {
                // CREATE LEFT AND RIGHT NODE FOR TEMP
                // PASS FIRST HALF OF STRING TO LEFT
                // PASS SECOND HALF OF STRING TO RIGHT
                temp.Left = CreateLeaf(S.Substring(0, (int)S.Length / 2));
                temp.Right = CreateLeaf(S.Substring((int)S.Length / 2, S.Length - (int)S.Length / 2));
                temp.SubLength = S.Substring(0, (int)S.Length / 2).Length;

                // HALF SPLIT STRING FOUND AT FOLLOWING LINK
                // https://www.codeproject.com/questions/1081689/how-do-i-split-my-my-string-in-equal-half-and-stor

            }

        }// END OF ROPE CONSTRUCTOR


        /*------------------------------------------------
        |
        |   Name: CreateLeaf
        |
        |   Description: Creates a leaf node which stores
		|				 a half string
        |   
        ------------------------------------------------*/
        private Node CreateLeaf(string S)
        {
            Node temp = new Node();

            // STORE HALF STRING
            if (S.Length <= 10)
            {
                temp.SubLength = S.Length;
                temp.SubString = S;
            }

            // CUT HALF STRING AGAIN
            else
            {
                // CREATE LEFT AND RIGHT NODE FOR TEMP
                // PASS FIRST HALF OF STRING TO LEFT
                // PASS SECOND HALF OF STRING TO RIGHT

                temp.Left = CreateLeaf(S.Substring(0, (int)S.Length / 2));
                temp.Right = CreateLeaf(S.Substring((int)S.Length / 2, S.Length - (int)S.Length / 2));
                temp.SubLength = S.Substring(0, (int)S.Length / 2).Length;

                // HALF SPLIT STRING FOUND AT FOLLOWING LINK
                // https://www.codeproject.com/questions/1081689/how-do-i-split-my-my-string-in-equal-half-and-stor
            }

            return temp;
        }// END OF CREATE LEAF


        /*------------------------------------------------
        |
        |   Name: Insert
        |
        |   Description: Insert a string at a specified
        |                index
        |   
        ------------------------------------------------*/
        public bool Insert(string S, int i)
        {
            List<Node> nodes;
            Node temp;
            Rope insert = new Rope(S);

            Console.WriteLine("\n... INSERT {0} AT {1} ...", S, i);

            int size = root.Left.SubLength;

            if (i > root.SubLength)
                return false;

            else
            {
                nodes = Split(i);

                // INSERT ON LEFT
                if (i < size)
                {
                    temp = Concatenate(nodes[1].Left, insert.root.Left);
                    temp.SubLength = nodes[1].SubLength;

                    root.Left = Concatenate(temp, root.Left);
                }

                // INSERT ON RIGHT SIDE
                else if (i >= size)
                {
                    temp = Concatenate(insert.root.Left, nodes[1].Left);
                    temp.SubLength = insert.root.SubLength;

                    root.Left = Concatenate(root.Left, temp);
                }
                return true;
            }


        }// END OF INSERT


        /*------------------------------------------------
        |
        |   Name: Delete
        |
        |   Description: Delete a string between two 
        |                indices
        |   
        ------------------------------------------------*/
        public bool Delete(int i, int j)
        {
            List<Node> nodes;
            Node temp;

            if (i > j || i > root.SubLength || j > root.SubLength)
                return false;

            Console.WriteLine("\n... DELETE FROM {0} TO {1} ...", i, j);

            int size = root.Left.SubLength;

            // FIRST SPLIT ON LEFT
            if (i < size)
            {
                nodes = Split(i);
                temp = nodes[1];
                ReBalance();
                nodes = Split(j - i);

                // CONCATENATE IN PROPER ORDERING
                if (j < size)
                    root.Left = Concatenate(temp, root.Left);
                else
                    root.Left = Concatenate(temp, nodes[1]);
            }

            // FIRST SPLIT ON RIGHT
            // SPLIT FURTHEST RIGHT TO LEFT
            // DONE THIS WAY TO PRESERVE AS MUCH OF ORIGINAL AS POSSIBLE
            else
            {
                nodes = Split(j);
                temp = nodes[1];
                ReBalance();
                nodes = Split(i);

                // CONACTENATE IN PROPER ORDERING
                if (i >= size)
                    root.Left = Concatenate(root.Left, temp);
                else
                    root.Left = Concatenate(nodes[1], temp);
            }

            return true;

        }// END OF DELETE


        /*------------------------------------------------
        |
        |   Name: Substring
        |
        |   Description: Returns a string between two
        |                indices
        |   
        ------------------------------------------------*/
        public string Substring(int i, int j)
        {
            Console.WriteLine("\n... SUBSTRING OF ROPE FROM {0} --> {1} ...\n", i, j);

            // SUBSTRING RANGE NOT IN CURRENT ROPE
            if (i > root.SubLength || j > root.SubLength || j < i)
                return ("THERE IS NO SUBSTRING THAT EXISTS IN THIS RANGE");

            return FindString(root, i, j, "");

            // FINDS THE SUBSTRING IN THE RANGE OF I --> J
            string FindString(Node curr, int k, int l, string found_string)
            {
                // SUBSTRING STARTS AND ENDS ON LEFT
                if (k < curr.SubLength && l < curr.SubLength && curr.Left != null)
                    return FindString(curr.Left, k, l, found_string);

                // SUBSTRING STARTS AND ENDS ON RIGHT
                else if ((k >= curr.SubLength && l >= curr.SubLength) && curr.Right != null)
                    return FindString(curr.Right, k - curr.SubLength, l - curr.SubLength, found_string);

                // SUBSTRING STARTS AT LEFT AND CONTINUES ON RIGHT
                else if ((k < curr.SubLength && l > curr.SubLength) && (curr.Left != null && curr.Right != null))
                    return (FindString(curr.Left, k, curr.SubLength - 1, found_string) + FindString(curr.Right, 0, l - curr.SubLength, found_string));

                // RETURN SUBSTRING FROM K --> L
                else if (curr.SubString != null)
                {
                    if (l > curr.SubLength)
                        l = curr.SubLength;

                    // IF TRYING TO READ FROM K PAST SUBSTRING
                    if (k + (l - k) > curr.SubLength)
                        return curr.SubString.Substring(k, curr.SubLength - k + 1);

                    else
                        return curr.SubString.Substring(k, l - k + 1);
                }

                else
                    return " ";

            }// END OF FIND STRING

        }// END OF SUBSTRING


        /*------------------------------------------------
        |
        |   Name: CharAt
        |
        |   Description: Returns the character at a
        |                specified position
        |   
        ------------------------------------------------*/
        public char CharAt(int i)
        {
            // INDEX NOT IN ROPE
            if (i > root.SubLength)
            {
                Console.WriteLine("\n... THE INDEX {0} DOES NOT EXIST IN THIS ROPE ...\n", i);
                return ' ';
            }

            Console.WriteLine("\n... FINDING THE CHARACTER AT INDEX {0} ...\n", i);

            return FindChar(root.Left, i);

            // FIND CHARACTER AT INDEX I
            char FindChar(Node curr, int j)
            {
                // INDEX IS ON RIGHT
                if (curr.SubLength <= j && curr.Right != null)
                    return FindChar(curr.Right, j - curr.SubLength);

                // INDEX IS ON LEFT
                else if (curr.SubLength > j && curr.Left != null)
                    return FindChar(curr.Left, j);

                // INDEX IS AT CURRENT 
                else
                    return curr.SubString.ToCharArray()[j];

            }// END OF FIND CHAR

        }// END OF CHAR AT


        /*------------------------------------------------
        |
        |   Name: Split
        |
        |   Description: Returns two ropes split at a 
        |                specified position
        |   
        ------------------------------------------------*/
        public List<Node> Split(int i)
        {
            List<Node> nodes = new List<Node>();
            Node newRoot = new Node();

            Console.WriteLine("\n... SPLIT ROPE AT {0} ...\n", i);

            // SPLIT IS PAST ROPE LENGTH
            if (i > root.SubLength)
            {
                Console.WriteLine("\n... CANNOT SPLIT ROPE AT THIS LOCATION ...\n");
                return nodes;
            }

            // SPLIT OCCURS ON THE LEFT SIDE
            else if (i < root.Left.SubLength)
                newRoot.Left = SplitRope(root.Left, i, 0, new Node());

            // SPLIT OCCURS ON THE RIGHT SIDE
            else if (i > root.Left.SubLength)
                newRoot.Left = SplitRope(root.Left, i, 3, new Node());

            // SPLIT AT ROOT LEFT
            else if (i.Equals(root.Left.SubLength))
            {
                // ASSIGN NEW ROOT TO THE RIGHT ROPE OF THE ORIGINAL 
                newRoot.Left = root.Left.Right;
                newRoot.SubLength = root.SubLength - root.Left.SubLength;

                // RELINK ROOT OF ORIGINAL ROPE
                root.Left.Right = null;
                root.SubLength = root.Left.SubLength;
                root = root.Left;
            }

            // ADD ROOT NODES OF EACH ROPE TO A LIST OF NODES
            nodes.Add(root);
            nodes.Add(newRoot);

            Console.WriteLine("\t{0}", GetRopeString(nodes[0], ""));
            Console.WriteLine("\n\t{0}", GetRopeString(nodes[1], ""));

            return nodes;

            // TRAVERSES THROUGH ROPE AND SPLIT AT THE PROPER LOCATION
            // RE-LINKS APPROPRIATE NODES TO THE NEW ROPE ROOT
            Node SplitRope(Node curr, int j, int directions, Node tempRoot)
            {
                // SPLIT IS ON LEFT
                if (j < curr.SubLength && curr.Left != null)
                {
                    // INITIALLY GONE TO LEFT
                    if (directions < 2)
                    {
                        // ONLY GONE LEFT
                        if (directions.Equals(0))
                            tempRoot = LinkRoot(0);

                        tempRoot = SplitRope(curr.Left, j, directions, tempRoot);
                    }

                    // INITIALLY GONE TO RIGHT
                    else
                    {
                        // ONLY GONE RIGHT
                        if (directions.Equals(3))
                            tempRoot = LinkRoot(2);

                        // GONE LEFT AT LEAST ONCE
                        else
                            tempRoot = LinkNew(1);

                        tempRoot = SplitRope(curr.Left, j, 2, tempRoot);
                    }
                }

                // SPLIT IS ON RIGHT
                else if (j > curr.SubLength && curr.Right != null)
                {
                    // INITIALLY GONE TO THE LEFT
                    if (directions < 2)
                    {
                        // ONLY GONE LEFT
                        if (directions.Equals(0))
                            tempRoot = LinkRoot(1);

                        // GONE RIGHT AT LEAST ONCE
                        else
                            tempRoot = LinkNew(0);

                        tempRoot = SplitRope(curr.Right, j - curr.SubLength, 1, tempRoot);
                    }

                    // INITIALLY GONE TO THE RIGHT
                    else
                    {
                        // ONLY GONE RIGHT
                        if (directions.Equals(3))
                            tempRoot = LinkRoot(3);

                        tempRoot = SplitRope(curr.Right, j - curr.SubLength, directions, tempRoot);
                    }
                }

                // SPLIT IS BETWEEN LEFT AND RIGHT
                else if (j.Equals(curr.SubLength))
                {
                    // INITIALLY GONE TO LEFT
                    if (directions < 2)
                    {
                        if (directions.Equals(0))
                            tempRoot = LinkRoot(1);

                        else if (directions.Equals(1))
                            tempRoot = LinkNew(0);
                    }

                    // INITIALLY GONE TO RIGHT
                    else
                    {
                        if (directions.Equals(3))
                            tempRoot = LinkRoot(2);

                        else if (directions.Equals(2))
                            tempRoot = LinkNew(1);
                    }
                }

                // SPLIT IS AT CURRENT NODE
                // DOES NOT MATTER IF GONE LEFT OR RIGHT
                else
                {
                    curr.Left = CreateLeaf(curr.SubString.Substring(0, j));
                    curr.Right = CreateLeaf(curr.SubString.Substring(j, curr.SubLength - j));
                    curr.SubLength = curr.Left.SubLength;

                    curr.SubString = "";

                    tempRoot = SplitRope(curr, j, directions, tempRoot);
                }

                return tempRoot;

                // ASSIGN TEMP ROOT
                Node LinkRoot(int link)
                {
                    // LINK LEFT NODE TO TEMP ROOT
                    if (link < 2)
                    {
                        tempRoot.Left = curr.Left;
                        tempRoot.SubLength = curr.SubLength;

                        if (link.Equals(1))
                            curr.Left = null;
                    }

                    // LINK RIGHT NODE TO TEMP ROOT
                    else
                    {
                        tempRoot.Right = curr.Right;
                        tempRoot.SubLength = curr.SubLength;

                        if (link.Equals(2))
                            curr.Right = null;
                    }

                    return tempRoot;
                }// END OF LINK ROOT


                // LINK NODE TO NEW ROPE
                Node LinkNew(int link)
                {
                    Node trav = tempRoot;

                    // LINK TO RIGHT SIDE OF NEW ROPE
                    // FROM LEFT SIDE
                    if (link.Equals(0))
                    {
                        //
                        if (trav.Right == null)
                            trav.Right = curr.Left;

                        else
                        {
                            // FIND FURTHEST RIGHT NODE
                            while (trav.Right.Right != null)
                                trav = trav.Right;

                            trav.Right = Concatenate(trav.Right, curr.Left);
                        }


                        curr.Left = null;
                    }

                    // LINK TO LEFT SIDE OF NEW ROPE
                    // FROM RIGHT SIDE
                    else if (link.Equals(1))
                    {
                        if (trav.Left == null)
                            trav.Left = curr.Right;

                        else
                        {
                            // FIND FURTHEST LEFT NODE
                            while (trav.Left.Left != null)
                                trav = trav.Left;

                            trav = Concatenate(curr.Right, trav.Left);
                        }

                        curr.Right = null;
                    }

                    return tempRoot;
                }// END OF LINK NEW
            }// END OF SPLIT ROPE
        }// END OF SPLIT


        /*------------------------------------------------
        |
        |   Name: Concatenate
        |
        |   Description: Concatenates two ropes
        |   
        ------------------------------------------------*/
        public Node Concatenate(Node r1, Node r2)
        {
            Node temp = new Node
            {
                Left = r1,
                Right = r2,
                SubLength = r1.SubLength
            };

            return temp;
        }// END OF CONCATENATE


        /*------------------------------------------------
        |
        |   Name: Print
        |
        |   Description: Print the string represented by
        |                the rope
        |   
        ------------------------------------------------*/
        public void Print()
        {
            Console.WriteLine("\n... PRINTING ROPE ...\n\n\t{0}", GetRopeString(root.Left, ""));
        }// END OF PRINT


        /*------------------------------------------------
        |
        |   Name: GetRopeString
        |
        |   Description: Traverse rope to get
        |                the full string stored
        |   
        ------------------------------------------------*/
        private string GetRopeString(Node curr, string ropeString)
        {
            // GO TO LEFT
            if (curr.Left != null)
                ropeString = GetRopeString(curr.Left, ropeString);

            // GO TO RIGHT
            if (curr.Right != null)
                ropeString = GetRopeString(curr.Right, ropeString);

            // AT LEAF --> GET STRING STORED 
            if (curr.SubString != null)
                ropeString += curr.SubString;

            else
                return ropeString;

            return ropeString;

        }// END OF GET ROPE STRING


        /*------------------------------------------------
        |
        |   Name: ReBalance
        |
        |   Description: Quick way of rebalancing rope
        |                
        |      ** Does not rebalance in the way described
        |         in online papers. Retrieves full text
        |         builds a new rope
        |   
        ------------------------------------------------*/
        public void ReBalance()
        {
            Rope rebalance = new Rope(GetRopeString(root.Left, ""));

            root = rebalance.root;
        }// END OF REBALANCE


    }// END OF ROPE CLASS

}// END OF CULIN-A2-B
