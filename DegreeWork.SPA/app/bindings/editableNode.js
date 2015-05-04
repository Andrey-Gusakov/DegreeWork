define(['knockout', 'jquery', 'lodash', 'durandal/composition'], function(ko, $, _, composition) {
    function composeEditor($element, context, bindingContext) {
        if(!_.isFunction(context.model.getEditor)) {
            throw new Error('incorrect usage');
        }

        var editor = context.model.getEditor();
        $editor = $element.clone();
        $editor.insertAfter($element);

        setEditorEvents(editor, $editor, $element);

        var valueAccessor = function() {
            var newSettings = {
                model: editor,
                area: 'editor-templates',
            };

            _.assign(newSettings, context.editorOptions);
            return newSettings;
        }

        var editorElement = $editor.get(0);
        ko.bindingHandlers.compose.init(editorElement, valueAccessor);
        ko.bindingHandlers.compose.update(editorElement, valueAccessor, null, null, bindingContext);

        return $editor;
    }

    function setEditorEvents(editor, $editor, $element) {
        $editor.on('click', function(event) {
            event.stopPropagation();
        })

        $editor.on('turn-card', function(event) {
            if(!editor.saveChanges()) {
                addBlur($editor);
            }
            else {
                $element.removeClass('hidden');
                $editor.addClass('hidden');
                $editor.removeClass('focused-card');
            }
        });
    }

    function addBlur($editor) {
        $(document).one('click', function() {
            $editor.trigger('turn-card');
        });
    }

    function applyEditor(element, settings, bindingContext) {
        var strategyImplementation = _.isFunction(settings.strategy) ? settings.strategy : composition.defaultStrategy;

        return function(context) {
            


            return strategyImplementation(context, element);
        }
    }

    ko.bindingHandlers.editableNode = {
        init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var $element = $(element);
            
            var context = ko.unwrap(valueAccessor());
            if(!_.isObject(context.model)) {
                context = {
                    model: context
                };
            }

            var $editor;
            $element.on('click', function(event) {
                if(!$editor) {
                    $editor = composeEditor($element, context, bindingContext);
                }

                $element.addClass('hidden');
                $editor.removeClass('hidden');
                $editor.addClass('focused-card');

                addBlur($editor);
                event.stopPropagation();
            });

            var result = ko.bindingHandlers.compose.init(element, valueAccessor);
            ko.bindingHandlers.compose.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

            return result;
        }
    };
});