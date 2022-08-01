<template>
    <div>
        <vs-list-item :title="this.todoTask.text" class="h-10" :style="todoTaskStyle">
            <template slot="avatar">
                <vs-checkbox v-model="taskDone" color="primary" @change="check($event)" />
            </template>
            <div class="flex">
                <vs-button size="small" color="primary" type="flat" icon="edit" @click="editTask()"></vs-button>
                <vs-button size="small" color="danger" type="flat" icon="delete" @click="removeTask()"></vs-button>
            </div>
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
            console.log('todoTaskStyle', this.todoTask, this.taskDone);
            return {
                'text-decoration': this.taskDone ? 'line-through' : '',
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
        },
        async removeTask() {
            const response = await todoListService.removeTask(this.todoTask.id);
            if (response === 1) {
                this.$emit("updateList");
            }
        },
        editTask() {
            this.$emit("edit", this.todoTask);
        }
    }
}
</script>
<style>
</style>