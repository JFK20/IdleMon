from os import path
import re
from json import dump

class Move:
    
    def __init__(self, name, type, category, pp, power, accuracy, description):
        self.name = name
        self.type = type
        self.category = category
        self.pp = pp
        self.power = power
        self.accuracy = accuracy
        self.description = description
        
    def toJson(self):
        return {
            "Name": self.name,
            "Type": self.type,
            "Category": self.category,
            "PP": self.pp,
            "Power": self.power,
            "Accuracy": self.accuracy,
            "Description": self.description
        }
        

def getTexts(file_name):
    text = []
    with open(file_name, "r") as data_file:
        combined = ""
        for line in data_file:
            if line.startswith("#"):
                if combined != "":
                    text.append(combined)
                    combined = ""
            else:
                combined += line
        else:
            if combined != "":
                text.append(combined)
            
    return text     
       


def textToMove(texts):
    Moves = []
    
    patternName = r'Name\s*=\s*(\w+ *\w*)'
    patternType = r"Type\s*=\s*(\w+ *\w*)"
    patternCategory = r"Category\s*=\s*(\w+ *\w*)"
    patternPP = r"TotalPP\s*=\s*(\w+ *\w*)"
    patternPower = r"Power\s*=\s*(\w+ *\w*)"
    patternAccuracy = r"Accuracy\s*=\s*(\w+ *\w*)"
    patternDescription = r"Description\s*=\s*((?:\w+,*.*))"
    
    matchName = None
    matchType = None
    matchCategory = None
    matchPP = None
    matchPower = None
    matchAccuracy = None
    matchDescription = None
    
    for text in texts:
        matchName = re.search(patternName, text)
        matchType = re.search(patternType, text)
        matchCategory = re.search(patternCategory, text)
        matchPP = re.search(patternPP, text)
        matchPower = re.search(patternPower, text)
        matchAccuracy = re.search(patternAccuracy, text)
        matchDescription = re.search(patternDescription, text)
        if matchName and matchType and matchCategory and matchPP and matchAccuracy and matchDescription:
            if matchPower:
                move = Move(matchName.group(1),matchType.group(1), matchCategory.group(1), matchPP.group(1), matchPower.group(1), matchAccuracy.group(1), matchDescription.group(1))
            else:   
                move = Move(matchName.group(1),matchType.group(1), matchCategory.group(1), matchPP.group(1), "", matchAccuracy.group(1), matchDescription.group(1))
            #print(move.toJson())
            Moves.append(move)
        
    return Moves

def dumpMoves(Moves):
    output_file = path.join(absolute_path, "Moves.json")
    
    jsonMoves = [move.toJson() for move in Moves]
    
    with open(output_file, "w") as output_file:
        dump(jsonMoves, output_file, indent=4)
            
                

if __name__ == "__main__":
    
    absolute_path = path.dirname(__file__)
    moves_file = path.join(absolute_path, "moves.txt")
    
    texts = getTexts(moves_file)
    Moves = textToMove(texts)
    dumpMoves(Moves)
    print("done")