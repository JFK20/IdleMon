from os import path
import re
from json import dump


class Pokemon:

    def __init__(self, name, types, baseStats, genderRatio, ability, hiddenAbilities, moveSet, habitat, evolutions, description):
        self.name = name
        self.types = types
        self.baseStats = baseStats
        self.genderRatio = genderRatio
        self.ability = ability
        self.hiddenAbilities = hiddenAbilities
        self.moveSet = moveSet
        self.habitat = habitat
        self.evolutions = evolutions
        self.description = description

    def toJson(self):
        return {
            "name": self.name,
            "types": self.types,
            "baseStats": self.baseStats,
            "genderRatio": self.genderRatio,
            "ability": self.ability,
            "hiddenAbilities": self.hiddenAbilities,
            "moveSet": self.moveSet,
            "habitat": self.habitat,
            "evolutions": self.evolutions,
            "description": self.description
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


def textToPokemons(texts):
    pokemons = []

    patternName = r'Name\s*=\s*(\w+ *\w*)'
    patternType = r'Types\s*=\s*((?:\w+,*)+)'
    patternBaseStats = r'BaseStats\s*=\s*(\d+,\d+,\d+,\d+,\d+,\d+)'
    patternGender = r'GenderRatio\s*=\s*(\w+)'
    patternAbility = r'\bAbilities\s*=\s*((?:\w+,*)+)'
    patternHiddenAbility = r'HiddenAbilities\s*=\s*((?:\w+,*)+)'
    patternMoves = r'Moves\s*=\s*((?:\w+,*)+)'
    patternHabitat = r'Habitat\s*=\s*(\w+)'
    patternEvolutions = r'Evolutions\s*=\s*((?:\w+,*)+)'
    patternDescription = r"Pokedex\s*=\s*((?:\w+,*.*))"

    matchName = None
    matchType = None
    matchBaseStats = None
    matchGender = None
    matchAbility = None
    matchHiddenAbility = None
    matchMoves = None
    matchHabitat = None
    matchEvolutions = None
    matchDescription = None

    for text in texts:
        matchName = re.search(patternName, text)
        matchType = re.search(patternType, text)
        matchBaseStats = re.search(patternBaseStats, text)
        matchGender = re.search(patternGender, text)
        matchAbility = re.search(patternAbility, text)
        matchHiddenAbility = re.search(patternHiddenAbility, text)
        matchMoves = re.search(patternMoves, text)
        matchHabitat = re.search(patternHabitat, text)
        matchEvolutions = re.search(patternEvolutions, text)
        matchDescription = re.search(patternDescription, text)
        print(matchDescription.group(1))

        if matchName and matchType and matchBaseStats and matchGender and matchAbility and matchMoves and matchHabitat and matchDescription:
            print("Stage2")
            if matchHiddenAbility:
                matchHiddenAbility = matchHiddenAbility.group(1)
            else:
                matchHiddenAbility = ""

            if matchEvolutions:
                matchEvolutions = matchEvolutions.group(1)
            else:
                matchEvolutions = ""

            if matchHabitat:
                matchHabitat = matchHabitat.group(1)
            else:
                matchHabitat = ""

            pokemon = Pokemon(matchName.group(1), matchType.group(1), matchBaseStats.group(1), matchGender.group(1), matchAbility.group(
                1), matchHiddenAbility, matchMoves.group(1), matchHabitat, matchEvolutions, matchDescription.group(1))

            pokemons.append(pokemon)
            print(pokemon.toJson())

    return pokemons


def dumpPokemons(pokemons):
    output_file = path.join(absolute_path, "Pokemons.json")

    jsonPokemons = [pokemon.toJson() for pokemon in pokemons]

    with open(output_file, "w") as output_file:
        dump(jsonPokemons, output_file, indent=4)


if __name__ == "__main__":

    absolute_path = path.dirname(__file__)
    pokemon_file = path.join(absolute_path, "Pokemon.txt")

    texts = getTexts(pokemon_file)
    pokemons = textToPokemons(texts)
    dumpPokemons(pokemons)
    print("done")
