<template>
    <div>
        <vs-list-item :title="this.todoTask.text">
            <vs-checkbox color="primary" :value="this.todoTask.statusId !== 1" />
        </vs-list-item>
    </div>
</template>
<script>
export default {
    name: 'TodoTask',
    components: {},
    props: {
        todoTask: Object
    },
    data: () => ({})
}
</script>
<style>
</style>