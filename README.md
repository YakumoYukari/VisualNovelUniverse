# VisualNovelUniverse

Visual Novel Universe tool for cataloging, organizing, and discovering visual novels.

It's basically for people with really big, unmaintainable VN collections.
If you have so many VNs you don't remember them all anymore, this might just help you.

The program has 2 main features: Organizing and Discovering. Let's cover the operation of both of those.

# Organizing

1. The first thing you need to do is make sure all your VNs are in one place. Or multiple places.
The important thing to remember is that wherever they are, there can only be VN folders there.

When importing, Visual Novel Universe assumes every folder and file in the directories you
point it to is its own VN.

2. Launch the program and go to File -> Choose Directory. Here you can change your active directories to point where your VNs are.
Just point it to the folder where they are all contained, not each individual folder.

3. Watch your list populate with all the folder names.

4. Go to Tools -> Import All New VNs. This will clean up the folder names, and put any loose files (such as "VN.rar") into
their own folders. If your folders were messy before, they should appear somewhat cleaner now.

5. You need to confirm what each VN is. This might take a while if you have a lot of VNs. The steps are simple, though:
  - Click on the VN in the list.
  - If VNDB takes you directly to a VN page (e.g. the folder name was precise enough),
       that VN will now be associated automatically.
  - If it doesn't find an exact match, click on the correct one in the search results (or search again)
       and while the VN in question is the selected one in the list and the correct VNDB page is open,
       either click the confirm link next to the title or go to Data -> Confirm VN
       
6. The VN will be populated, and you will get a VN Info.xml file and a VNDB Cover Image.jpg file in that directory.

7. If VNDB is missing any info, or it needs to be manually tuned, you can edit the VN Info.xml file. Be careful, though.

# Discovering New VNs

1. Any time you go to a VNDB entry page, that VN's English / Japanese name will appear at the bottom.
That is your active VN for searching. All searches will be for that VN.

2. Customize your searches to have whatever websites you want. Go to that site, search for something,
then take the URL and replace the term you searched for with {0} when creating new entries.

3. Simply use the English or Japanese search buttons to search for any VN on VNDB.

# TODO

1. Save the VN List columns display options
2. Keyboard shortcuts
3. Continue development of more features yet to be decided

# Feedback

I'd love to hear your ideas, and am always open to pull requests. Please don't hesitate to add your own features or clean up my code!
