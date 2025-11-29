#!/bin/env dotnet

using System;
using System.Collections.Generic;

if ( args.Count() < 3 )
{
    Console.WriteLine( @"
NAME

    santa.csx

SYNOPSIS

    santa.csx [NAME NAME [NAME ...]]

DESCRIPTION

    Generates pairs of names, where each 'giver' is guaranteed to differ
    from each 'recipient'. Therefore, it can be used to make so-called
    'secret Santa' pairings.
" );
}

List<string> names = new( args.Select( x => x.Trim() )
                              .Distinct()
                        );

while ( true )
{
    Dictionary<string,string> dictionary = new ();

    // reshuffle the values
    Stack<string> values = new ( names.Shuffle() );

    foreach ( var name in names )
    {
        var recipient = values.Pop();

        // if giver matches recipient, just abandon and start over
        if ( recipient == name ) goto tryagain;

        dictionary[ name ] = recipient;
    }

    foreach ( var pair in dictionary )
    {
        Console.WriteLine( $"{pair.Key}\tgives to {pair.Value}" );
    }

    break;

    tryagain:

    continue;
}

