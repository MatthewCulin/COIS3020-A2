/*
|   
|   FILE TYPE:      C# PROGRAM
|   AUTHOR:         MATTHEW CULIN
|   DATE:           FEBRUARY 2020
|       
|   PURPOSE:        PROGRAM TO CREATE ROPES
|   USEAGE:         Culin-A2-a.exe
|
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Culin_A2_A
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }// END OF I CONTAINER

    public interface ITrie<T> : IContainer<T>
    {
        bool Insert(string key, T value);
        bool Remove(string key);
        T Value(string key);
    }// END OF ITRIE

    public interface ITernary<T> : IContainer<T>
    { 
        bool Insert(string key, T value);
        T Value(string key);
    }// END OF ITERNARY



    /*----------------------------------------------------------------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |------------------------------------------- PROGRAM CLASS -------------------------------------------|
    |-----------------------------------------------------------------------------------------------------|
    |----------------------------------------------------------------------------------------------------*/
    public class Program
    {
        /*------------------------------------------------
        |   
        |   Name: main()
        |
        |   Description: Main method that runs program
        |
        ------------------------------------------------*/
        static void Main(string[] args)
        {
            RWay<int> R = new RWay<int>();
            Ternary<int> T = new Ternary<int>();


            // R-WAY TREE INSERT
            Console.WriteLine("\n\t......................");
            Console.WriteLine("\t..... R-WAY TREE .....");
            Console.WriteLine("\t......................");
            R.Insert("Brian", 10);
            R.Insert("Brianna", 20);
            R.Insert("Yvonne", 70);
            R.Insert("Adrian", 30);
            R.Insert("Adam", 40);
            R.Insert("B", 50);
            R.Insert("George", 60); 
            Console.WriteLine("\nSize of R-Way Tree (Insert) --> {0}", R.Size());

            // R-WAY TREE AUTO COMPLETE
            R.AutoComplete("Brian");

            // R-WAY TREE PARTIAL MATCH
            R.PartialMatch("..o..e");

            // R-WAY TREE REMOVE
            R.Remove("B");
            R.Remove("Bill");
            R.Remove("Brian");
            R.Remove("Adam");
            Console.WriteLine("\nSize of R-Way Tree (Remove) --> {0}", R.Size());

            // R-WAY TREE AUTO COMPLETE
            R.AutoComplete("Brian");

            // R-WAY TREE PRINT
            Console.WriteLine("\nPrinting R-Way Tree\n");
            R.Print();
            Console.ReadKey();

            // TERNARY TREE INSERT
            Console.WriteLine("\n\t........................");
            Console.WriteLine("\t..... TERNARY TREE .....");
            Console.WriteLine("\t........................");
            T.Insert("bag", 10);
            T.Insert("bat", 20);
            T.Insert("car", 80);
            T.Insert("cab", 70);
            T.Insert("bagel", 30);
            T.Insert("beet", 40);
            T.Insert("abc", 60);
            T.Insert("cat", 90);
            
            // TERNARY TREE AUTO COMPLETE
            T.AutoComplete("bag");

            // TERNARY TREE PARTIAL MATCH
            T.PartialMatch(".a.");

            // TERNARY TREE PRINT
            Console.WriteLine("\nPRINTING TERNARY TREE\n");
            T.Print();

            // TERNARY TREE STATISTICS
            Console.WriteLine("\nSize of Ternary Tree --> {0}", T.Size());
            Console.WriteLine("\nValue of ABC --> {0}", T.Value("abc"));
            Console.WriteLine("Value of BEET --> {0}", T.Value("beet"));
            Console.WriteLine("Value of A --> {0}", T.Value("a"));
            Console.WriteLine("\nContains BAET --> {0}", T.Contains("baet"));
            Console.WriteLine("Contains BEET --> {0}", T.Contains("beet"));
            Console.WriteLine("Contains ABC --> {0}\n", T.Contains("abc"));

            Console.ReadKey();
        }
        
    }// END OF PROGRAM CLASS




    /*-------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------|
    |------------------------------------------- RWAY CLASS -------------------------------------------|
    |--------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------*/
    public class RWay<T> : ITrie<T>
    {
        private Node root;          // Root node of the RWay Tree
        private char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        class Node
        {
            public T value;         // Value at Node; otherwise default
            public int numValues;   // Number of descendent values of a Node 
            public Node[] child;    // Branching for each letter 'a' .. 'z'

            // Node
            // Creates an empty Node
            // All children are set to null by default
            // Time complexity:  O(1)

            public Node()
            {
                value = default(T);
                numValues = 0;
                
                // HANDLES ALL LETTERS OF ALPHABET
                child = new Node[26];   
            }
        }

        // RWay
        // Creates an empty RWay Tree
        // Time complexity:  O(1)

        public RWay()
        {
            MakeEmpty();
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored
        // Time complexity:  O(L) where L is the length of the key

        private bool Insert(Node p, string key, int j, T value)
        {
            int i;

            if (j == key.Length)
            {
                if (p.value.Equals(default(T)))
                {
                    // Sets the value at the Node
                    p.value = value;
                    p.numValues++;
                    return true;
                }
                // Duplicate keys are ignored (unsuccessful insertion)
                else
                    return false;
            }
            else
            {
                // Maps a character to an index
                i = char.ToLower(key[j]) - 'a';

                // Creates a new Node if the link is null
                // Note: Node is initialized to the default value
                if (p.child[i] == null)
                    p.child[i] = new Node();

                // If the inseration is successful
                if (Insert(p.child[i], key, j + 1, value))
                {
                    // Increase number of descendent values by one
                    p.numValues++;
                    return true;
                }
                else
                    return false;
            }
        }

        // Value
        // Returns the value associated with a key; otherwise default
        // Time complexity:  O(L) where L is the length of the key

        public T Value(string key)
        {
            int i;
            Node p = root;

            // Traverses the links character by character
            foreach (char ch in key)
            {
                i = char.ToLower(ch) - 'a';
                if (p.child[i] == null)
                    return default(T);    // Key is too long
                else
                    p = p.child[i];
            }
            return p.value;               // Returns the value or default
        }

        // Public Remove
        // Calls the private Remove that carries out the actual deletion
        // Returns true if successful; false otherwise

        public bool Remove(string key)
        {
            return Remove(root, key, 0);
        }

        // Private Remove
        // Removes the value associated with the given key
        // Time complexity:  O(L) where L is the length of the key

        private bool Remove(Node p, string key, int j)
        {
            int i;

            // Key not found
            if (p == null)
                return false;

            else if (j == key.Length)
            {
                // Key/value pair found
                if (!p.value.Equals(default(T)))
                {
                    p.value = default(T);
                    p.numValues--;
                    return true;
                }
                // No value with associated key
                else
                    return false;
            }

            else
            {
                i = char.ToLower(key[j]) - 'a';

                // If the deletion is successful
                if (Remove(p.child[i], key, j + 1))
                {
                    // Decrease number of descendent values by one and
                    // Remove Nodes with no remaining descendents
                    if (p.child[i].numValues == 0)
                        p.child[i] = null;
                    p.numValues--;
                    return true;
                }
                else
                    return false;
            }
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = new Node();
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root.numValues == 0;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return root.numValues;
        }

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root,"");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys
        // Time complexity:  O(S) where S is the total length of the keys

        private void Print(Node p, string key)
        {
            int i;

            if (p != null)
            {
                //if (!p.value.Equals(default(T)))
                Console.WriteLine("\t" + key + " " + p.value + " " + p.numValues);
                for (i = 0; i < 26; i++)
                    Print(p.child[i], key+(char)(i+'a'));
            }
        }
    

        /*------------------------------------------------
        |   
        |   Name: PartialMatch()
        |
        |   Description: Finds all keys that contain the 
        |                passed pattern
        |                
        |                ie. .A.D --> BAND, CARD, ETC.
        |
        ------------------------------------------------*/
        public List<string> PartialMatch(string pattern)
        {
            List<string> keys = new List<string>();           
            Node curr = root;

            // SPLIT INTO CHARACTER ARRAY
            char[] characters = pattern.ToLower().ToCharArray();
            int i;

            keys = FindPattern(curr, characters, "", keys);

            if(keys[0] != null)
            {
                Console.WriteLine("\nKeys found in R-WAY tree with the pattern {0}", pattern);
                for(i = 0; i < keys.Count(); i++)
                    Console.WriteLine("\t{0}", keys[i]);
            }
            
            else
                Console.WriteLine("\nNo keys found in R-WAY tree with the pattern {0}", pattern);


            return keys;

            // RECURSIVELY TRAVERSE TREE TO FIND A KEY WITH THE PROVIDED PATTERN
            List<string> FindPattern(Node curr, char[] pattern, string found_key, List<string> keys)
            {
                // END OF PATTERN
                if(pattern.Length.Equals(0))
                {
                    if(Convert.ToInt32(curr.value) > 0)
                        keys.Add(found_key);

                    return keys;
                }

                // TRAVERSE TREE TO FIND PATTERN
                else
                {
                    for(int i = 0; i < curr.child.Length; i++)
                    {
                        // ANY LETTER OR MATCHING LETTER
                        if(curr.child[i] != null && (pattern[0].Equals('.')||(i+'a').Equals(pattern[0])))
                        {
                            string search_key = found_key + ((char) (i+'a')).ToString();
                            char[] subArray = SubArray(pattern, 1, pattern.Length-1);
                            keys = FindPattern(curr.child[i], subArray, search_key, keys);
                        }
                    }
                }

                return keys;

                // CREATE NEW ARRAY WITHOUT THE FIRST ELEMENT
                // https://stackoverflow.com/questions/943635/getting-a-sub-array-from-an-existing-array
                char[] SubArray(char[] original, int index, int length)
                {
                    char [] subArray = new char[length];

                    Array.Copy(original, index, subArray, 0, length);

                    return subArray;
                }// END OF SUB ARRAY
            }// END OF FIND KEY

        }// END OF PARTIAL MATCH
        
        /*------------------------------------------------
        |   
        |   Name: Autocomplete()
        |
        |   Description: Finds all keys that begin with 
        |                a passed prefix
        |                
        |                ie. CA --> CAT, CAR, ETC.
        |
        ------------------------------------------------*/
        public List<string> AutoComplete(string prefix)
        {
            List<string> keys = new List<string>();
            Node curr = root;

            // SPLIT INTO CHARACTER ARRAY
            char[] characters = prefix.ToLower().ToCharArray();
            int i;

            // FIND KEYS THAT BEGIN WITH PREFIX PROVIDED
            foreach(char value in characters)
            {
                for (i = 0; i < alpha.Length; i++)
                {
                    if(curr.child != null)
                    {
                        // MOVE TO CHILD[i]
                        if(value.Equals(alpha[i]))
                            curr = curr.child[i];
                    }
                }
            }

            keys = FindKey(curr, prefix, keys);

            if(keys[0] != null)
            {
                Console.WriteLine("\nKeys found in R-WAY tree with the prefix {0}", prefix);
                for(i = 0; i < keys.Count(); i++)
                    Console.WriteLine("\t{0}", keys[i]);
            }
            
            else
                Console.WriteLine("\nNo keys found in R-WAY tree with the prefix {0}", prefix);


            return keys;

            // RECURSIVELY TRAVERSE TREE TO FIND A KEY AND ADD IT TO THE LIST
            List<string> FindKey(Node p, string found_key, List<string> found_keys)
            {
                if(p.value.Equals(default(T)) && p.child != null)
                    RecursiveLoop();

                else
                {
                    // ADD KEY TO LIST
                    found_keys.Add(found_key);
                    
                    if(p.child != null)
                        RecursiveLoop();
                }
                
                // RECURSIVELY TRAVERSE TREE TO FIND A KEY
                void RecursiveLoop()
                {
                    int i;

                    for(i = 0; i < p.child.Length; i++)
                    {
                        // RECURSIVELY CALL FINDKEY TO TRAVERSE TREE
                        if(p.child[i] != null)
                        {
                            // APPEND CHILD TO END OF FOUND KEY
                            found_key += alpha[i].ToString();
                            FindKey(p.child[i], found_key, found_keys);
                        }

                    }

                }// END OF INTERNAL METHOD RECURSIVE LOOP

                return found_keys;
            }// END OF INTERNAL METHOD FIND KEY
        }// END OF AUTO COMPLETE
    }// END OF R WAY CLASS




    /*-------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------|
    |----------------------------------------- TERNARY CLASS ------------------------------------------|
    |--------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------*/
    public class Ternary<T> : ITernary<T>
    {
        private Node root;                 // Root node of the Ternary Tree
        private int size;                  // Number of values in the Trie

        class Node
        {
            public char ch;                // Character of the key
            public T value;                // Value at Node; otherwise default
            public Node low, middle, high; // Left, middle, and right subtrees

            // Node
            // Creates an empty Node
            // All children are set to null
            // Time complexity:  O(1)

            public Node(char ch)
            {
                this.ch = ch;
                value = default(T);
                low = middle = high = null;
            }
        }

        // Ternary
        // Creates an empty TernaryTree
        // Time complexity:  O(1)

        public Ternary ()
        {
            MakeEmpty();
            size = 0;
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(ref root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored

        private bool Insert(ref Node p, string key, int i, T value)
        {
            if (p == null)
                p = new Node(key[i]);

            // Current character of key inserted in left subtree
            if (key[i] < p.ch)
                return Insert(ref p.low, key, i, value);

            // Current character of key inserted in right subtree
            else if (key[i] > p.ch)
                return Insert(ref p.high, key, i, value);

            else if (i + 1 == key.Length)
            // Key found
            {
                // But key/value pair already exists
                if (!p.value.Equals(default(T)))
                    return false;
                else
                {
                    // Place value in node
                    p.value = value;
                    size++;
                    return true;
                }
            }

            else
                // Next character of key inserted in middle subtree
                return Insert(ref p.middle, key, i + 1, value);
        }

        // Value
        // Returns the value associated with a key; otherwise default

        public T Value(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;   
                          
                else // if (p.ch == key[i])
                {
                    // Return the value if all characters of the key have been visited 
                    if (++i == key.Length)  
                        return p.value;

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;           
                }
            }
            return default(T);   // Key too long
        }

        // Contains
        // Returns true if the given key is found in the Trie; false otherwise

        public bool Contains(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return true if the key is associated with a non-default value; false otherwise 
                    if (++i == key.Length)    
                        return !p.value.Equals(default(T));

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return false;        // Key too long
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = null;
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root == null;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root,"");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys 

        private void Print(Node p, string key)
        {
            if (p != null)
            {
                Print(p.low, key);
                if (!p.value.Equals(default(T)))
                    Console.WriteLine("\t" + key + p.ch + " " + p.value);
                Print(p.middle, key+p.ch);
                Print(p.high, key);
            }
        }
    

        /*------------------------------------------------
        |   
        |   Name: PartialMatch()
        |
        |   Description: Finds all keys that contain the 
        |                passed pattern
        |                
        |                ie. .A.D --> BAND, CARD, ETC.
        |
        ------------------------------------------------*/
        public List<string> PartialMatch(string pattern)
        {
            List<string> keys = new List<string>();  
            Node curr = root;         

            // SPLIT INTO CHARACTER ARRAY
            char[] characters = pattern.ToLower().ToCharArray();
            int i;

            keys = FindPattern(curr, characters, "", keys);

            if(keys[0] != null)
            {
                Console.WriteLine("\nKeys found in TERNARY tree with the pattern {0}", pattern);
                for(i = 0; i < keys.Count(); i++)
                    Console.WriteLine("\t{0}",keys[i]);
            }
            
            else
                Console.WriteLine("\nNo keys found in TERNARY tree with the pattern {0}", pattern);

            return keys;

            // TRAVERSE TREE TO FIND THE PATTERN GIVEN
            List<string> FindPattern(Node curr, char[] characters, string found_key, List<string> keys)
            {
                // IF ON LAST CHARACTER OF PATTERN
                if(characters.Length.Equals(1))
                {
                    // IF ANY CHARACTER
                    // ADD LEFT, RIGHT, AND MIDDLE
                    // IF THEY ARE A KEY
                    if(characters[0].Equals('.'))
                    {
                        if(curr.low != null)
                            keys = FindPattern(curr.low, characters, found_key, keys);

                        if(curr.high != null)
                            keys = FindPattern(curr.high, characters, found_key, keys);
                        
                        if(Convert.ToInt32(curr.value) > 0)
                            keys.Add(found_key+curr.ch);
                    }
                
                    // IF CHARACTER EQUALS CURRENT
                    // ADD CURRENT IF IT IS A KEY
                    else if(characters[0].Equals(curr.ch) && !curr.value.Equals(default(T)))
                        keys.Add(found_key+curr.ch);

                    // IF CHARACTER IS LESS THAN CURRENT
                    // GO TO LEFT
                    else if(characters[0] < curr.ch && curr.low != null)
                        keys = FindPattern(curr.low, characters, found_key, keys);

                    // IF CHARACTER IS GREATER THAN CURRENT
                    // GO TO RIGHT
                    else if(characters[0] > curr.ch && curr.high != null)
                        keys = FindPattern(curr.high, characters, found_key, keys);
                }

                // TRAVERSE TREE
                else
                {
                    // IF ANY CHARACTER --> GO TO LEFT THEN RIGHT THEN MIDDLE
                    if(characters[0].Equals('.'))
                    {
                        if(curr.low != null)
                            keys = FindPattern(curr.low, characters, found_key, keys);

                        if(curr.high != null)
                            keys = FindPattern(curr.high, characters, found_key, keys);

                        if(curr.middle != null)
                        {
                            found_key += curr.ch;
                            char[] subArray = SubArray(characters, 1, characters.Length-1);
                            keys = FindPattern(curr.middle, subArray, found_key, keys);
                        }
                    }

                    // IF CHARACTER EQUALS CURRENT --> GO TO MIDDLE
                    else if(curr.ch.Equals(characters[0]) && curr.middle != null)
                    {
                        found_key += curr.ch;
                        char[] subArray = SubArray(characters, 1, characters.Length-1);
                        keys = FindPattern(curr.middle, subArray, found_key, keys);
                    }

                    // IF CURRENT IS GREATER THAN CHARACTER[0] --> GO TO LEFT
                    else if(curr.ch > characters[0] && curr.low != null)
                        keys = FindPattern(curr.low, characters, found_key, keys);

                    // IF CURRENT IS LESS THAN CHARACTER[0] --> GO TO RIGHT
                    else if(curr.ch < characters[0] && curr.high != null)
                        keys = FindPattern(curr.high, characters, found_key, keys);
                }

                // CREATE NEW ARRAY WITHOUT THE FIRST ELEMENT
                // https://stackoverflow.com/questions/943635/getting-a-sub-array-from-an-existing-array
                char[] SubArray(char[] original, int index, int length)
                {
                    char [] subArray = new char[length];

                    Array.Copy(original, index, subArray, 0, length);

                    return subArray;
                }// END OF SUB ARRAY

                return keys;
            }// END OF FIND PATTERN

        }// END OF PARTIAL MATCH


        /*------------------------------------------------
        |   
        |   Name: AutoComplete()
        |
        |   Description: Finds all keys that begin with 
        |                a passed prefix
        |                
        |                ie. CA --> CAT, CAR, ETC.
        |
        ------------------------------------------------*/
        public List<String> AutoComplete(string prefix)
        {
            List<string> keys = new List<string>();           
            Node curr = root;

            // SPLIT INTO CHARACTER ARRAY
            char[] characters = prefix.ToLower().ToCharArray();
            int i;

            foreach(char letter in characters)
            {
                curr = FindLetter(curr, letter);
                if(curr.Equals(null))
                    return keys;
                
                // IF THE PREFIX IS A KEY
                else if(letter.Equals(characters[characters.Length-1]))
                {
                    if(curr.value.Equals(default(T)))
                        curr = curr.middle;

                    else
                    {
                        // ADD KEY TO LIST
                        keys.Add(prefix);
                        curr = curr.middle;
                    }
                }

                else
                    curr = curr.middle;
            }
            
            keys = FindKeys(curr, prefix, keys);

            if(keys[0] != null)
            {
                Console.WriteLine("\nKeys found in TERNARY tree with the prefix {0}", prefix);
                for(i = 0; i < keys.Count(); i++)
                    Console.WriteLine("\t{0}",keys[i]);
            }
            
            else
                Console.WriteLine("\nNo keys found in TERNARY tree with the prefix {0}", prefix);

            return keys;

            // RECURSIVELY TRAVERSE TREE TO FIND A NODE THAT MATCHES A CERTAIN LETTER
            Node FindLetter(Node curr, char letter)
            {
                if(curr != null)
                {
                    // MOVE TO MIDDLE
                    if(letter.Equals(curr.ch) && curr.middle != null)
                        return curr;

                    // MOVE TO LEFT SIDE OF TREE 
                    else if(letter < curr.ch && curr.low !=null)
                        curr = FindLetter(curr.low, letter);

                    // MOVE TO RIGHT SIDE OF TREE
                    else if(letter > curr.ch && curr.high !=null)
                        curr = FindLetter(curr.high, letter);

                    else
                    {
                        curr = null;
                        Console.WriteLine("No keys contain the prefix {0}", prefix);
                    }
                }

                return curr;
            }

            // RECURSIVELY TRAVERSE TREE TO FIND A KEY AND ADD IT TO THE LIST
            List<string> FindKeys(Node curr, string found_key, List<string> keys)
            {
                Node search;

                // IF CURRENT VALUE IS DEFAULT
                // DON'T ADD TO LIST
                if(curr.value.Equals(default(T)))
                    RecursiveLoop();
                
                // IF CURRENT VALUE IS NOT DEFAULT
                // ADD TO LIST
                else
                {
                    // ADD KEY TO LIST
                    keys.Add(found_key+curr.ch);
                    RecursiveLoop();
                }

                // RECURSIVELY CHECK LEFT, RIGHT AND MIDDLE FOR VALUES
                void RecursiveLoop()
                {
                    if(curr != null)
                    {
                        // MOVE TO LEFT
                        if(curr.low!=null)
                        {
                            search = curr.low;
                            keys = FindKeys(search, found_key, keys);
                        }

                        // MOVE TO RIGHT
                        if(curr.high!=null)
                        {
                            search = curr.high;
                            keys = FindKeys(search, found_key, keys);
                        }

                        // MOVE TO MIDDLE
                        if(curr.middle!=null)
                        {
                            found_key += curr.ch;
                            curr = curr.middle;
                            keys = FindKeys(curr, found_key, keys);
                        }
                    }
                }

                return keys;
            }
        }// END OF AUTO COMPLETE
    }// END OF TERNARY CLASS
}