SOURCE_DIRECTORY := $(dir $(realpath $(lastword $(MAKEFILE_LIST))))
CONFIGURATION := Release
GENERATED_FOLDER := generated

clean:
	dotnet clean -c $(CONFIGURATION)
	rm -rf $(GENERATED_FOLDER)

build: clean
	cd $(SOURCE_DIRECTORY)Net60 ; dotnet build -c $(CONFIGURATION)
	cd $(SOURCE_DIRECTORY)Net70 ; dotnet build -c $(CONFIGURATION)

copy-files: build
	mkdir -p $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net60
	cp -R Net60/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net60
	cp -R $(SOURCE_DIRECTORY)Net60/obj/$(CONFIGURATION)/net6.0/generated/System.Text.Json.SourceGeneration/System.Text.Json.SourceGeneration.JsonSourceGenerator/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net60

	mkdir -p $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net70
	cp -R Net70/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net70
	cp -R $(SOURCE_DIRECTORY)Net70/obj/$(CONFIGURATION)/net7.0/generated/System.Text.Json.SourceGeneration/System.Text.Json.SourceGeneration.JsonSourceGenerator/*.cs $(SOURCE_DIRECTORY)$(GENERATED_FOLDER)/Net70

.DEFAULT_GOAL := copy-files