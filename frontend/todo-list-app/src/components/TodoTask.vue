<template>
    <div>
        <vs-list-item :title="this.todoTask.text" class="h-10" :style="todoTaskStyle">
            <vs-checkbox v-model="taskDone" color="primary" @change="check($event)" />
        </vs-list-item>
    </div>
</template>
<script>
import todoListService from '../../services/todoListService';

export default {
    name: 'TodoTask',
    components: {},
    props: {
        todoTask: Object
    },
    data: () => ({
        taskDone: false
    }),
    computed: {
        todoTaskStyle() {
            return {
                'text-decoration': this.taskDone ? 'line-through' : 'normal',
            };
        }
    },
    beforeMount() {
        this.taskDone = this.todoTask.statusId === 2;
    },
    methods: {
        async check(e) {
            let todoTask = this.todoTask;
            todoTask.statusId = e.target.checked ? 2 : 1;
            await todoListService.updateTask(todoTask);
        }
    }
}
</script>
<style>
</style>