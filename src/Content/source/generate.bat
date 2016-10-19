@echo off
set count=5
setlocal EnableDelayedExpansion

for /l %%i in (0,1, 40) do (
	for /l %%j in (0, 1, 35) do (
		set "formattedValue=000000%%i"
		set "formattedColumn=000000%%j"
		@echo Tile!formattedValue:~-2!!formattedColumn:~-2!;%%i;%%j
		
	)
)