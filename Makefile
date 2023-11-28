SOURCE_DIRECTORY := $(dir $(realpath $(lastword $(MAKEFILE_LIST))))
PROJECT_FOLDER := $(SOURCE_DIRECTORY)/SourceGenRegressionTests
CONFIGURATION := Release
GENERATED_FOLDER := generated

clean:
	dotnet clean -c $(CONFIGURATION)
	rm -rf $(GENERATED_FOLDER)

build: clean
	dotnet build -c $(CONFIGURATION)

copy-files: build
	mkdir -p $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net60
	cp -R $(PROJECT_FOLDER)/obj/$(CONFIGURATION)/net6.0/generated/System.Text.Json.SourceGeneration/System.Text.Json.SourceGeneration.JsonSourceGenerator/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net60

	mkdir -p $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net70
	cp -R $(PROJECT_FOLDER)/obj/$(CONFIGURATION)/net7.0/generated/System.Text.Json.SourceGeneration/System.Text.Json.SourceGeneration.JsonSourceGenerator/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net70

	mkdir -p $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net80
	cp -R $(PROJECT_FOLDER)/obj/$(CONFIGURATION)/net8.0/generated/System.Text.Json.SourceGeneration/System.Text.Json.SourceGeneration.JsonSourceGenerator/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net80

.DEFAULT_GOAL := copy-files